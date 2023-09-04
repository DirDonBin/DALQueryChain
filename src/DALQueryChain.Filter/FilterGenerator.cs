using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Extensions;
using DALQueryChain.Filter.Models;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace DALQueryChain.Filter
{
    internal static class FilterGenerator
    {
        internal static IFilterableQueryChain<TEntity> FilterGenerate<TEntity, TFilterModel>(IFilterableQueryChain<TEntity> query, TFilterModel model, string name = "default")
            where TFilterModel : class
        {
            var filters = ReadFilterModel(model, name);

            if (filters is null || filters.Count == 0) return query;

            Expression? exp = null;

            var itemEntity = Expression.Parameter(typeof(TEntity), "entity");
            var itemFilter = Expression.Constant(model);

            foreach (var filter in filters)
            {
                if (filter.Value.Count == 0) continue;

                foreach (var flt in filter.Value)
                {
                    Expression? predicate = null;

                    var entityPropertyPath = flt.DestFieldName.Split(".");
                    var filterPropertyPath = flt.SrcFieldName.Split(".");

                    Expression propertyEntity = itemEntity;
                    Expression propertyFilter = itemFilter;

                    foreach (var prop in entityPropertyPath)
                        propertyEntity = Expression.Property(propertyEntity, prop);

                    foreach (var prop in filterPropertyPath)
                        propertyFilter = Expression.Property(propertyFilter, prop);

                    var nullConstant = Expression.Constant(null);

                    if (!propertyEntity.Type.IsAssignableTo(typeof(IEnumerable)) && !propertyFilter.Type.IsAssignableTo(typeof(IEnumerable)))
                    {
                        if (propertyEntity.Type.IsNullableType() && !propertyFilter.Type.IsNullableType())
                            propertyFilter = Expression.Convert(propertyFilter, propertyEntity.Type);

                        if (propertyFilter.Type.IsNullableType() && !propertyEntity.Type.IsNullableType())
                            propertyEntity = Expression.Convert(propertyEntity, propertyFilter.Type);
                    }

                    Func<Expression, Expression, Expression> contains = (left, right) =>
                    {
                        if (right.Type == typeof(string))
                        {
                            var invariant = Expression.Constant(flt.Settings.StringSensitiveCase
                                ? StringComparison.InvariantCulture
                                : StringComparison.InvariantCultureIgnoreCase);

                            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) })!;
                            return Expression.Call(left, method, right, invariant);
                        }
                        else
                        {
                            MethodInfo method = typeof(Enumerable).GetMethods()
                                .Single(x => x.Name == "Contains" && x.GetParameters().Length == 2);

                            Expression? defaultExp = null;

                            if (right.Type.IsNullableType())
                            {
                                defaultExp = Expression.NotEqual(right, nullConstant);
                                right = Expression.Convert(right, Nullable.GetUnderlyingType(right.Type)!);
                            }

                            if (left.Type.IsNullableType())
                            {
                                defaultExp = defaultExp is null
                                    ? Expression.NotEqual(left, nullConstant)
                                    : Expression.AndAlso(defaultExp, Expression.NotEqual(left, nullConstant));
                                left = Expression.Convert(left, Nullable.GetUnderlyingType(left.Type)!);
                            }

                            var genericType = right.Type.GetElementType() ?? right.Type.GetGenericArguments()[0];

                            var checkContains = Expression.Call(null, method.MakeGenericMethod(genericType), right, left);

                            return defaultExp is null
                                ? checkContains
                                : Expression.AndAlso(defaultExp, checkContains);
                        }
                    };

                    Func<Expression, Expression, Expression> equals = (left, right) =>
                    {
                        if (left.Type == typeof(string))
                        {
                            var invariant = Expression.Constant(flt.Settings.StringSensitiveCase
                                ? StringComparison.InvariantCulture
                                : StringComparison.InvariantCultureIgnoreCase);
                            
                            MethodInfo method = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) })!;
                            return Expression.Call(left, method, right, invariant);
                        }

                        return Expression.Equal(left, right);
                    };

                    foreach (var condition in flt.ConditionTypes)
                    {
                        var predicateCond = condition switch
                        {
                            QCFilterCondition.Equals => equals(propertyFilter, propertyEntity),
                            QCFilterCondition.NotEqual => Expression.NotEqual(propertyFilter, propertyEntity),
                            QCFilterCondition.Greater => Expression.GreaterThan(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QCFilterCondition.GreaterOrEqual => Expression.GreaterThanOrEqual(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QCFilterCondition.Less => Expression.LessThan(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QCFilterCondition.LessOrEqual => Expression.LessThanOrEqual(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QCFilterCondition.Contains => contains(propertyEntity, propertyFilter),
                            _ => throw new Exception()
                        };

                        if (flt.Settings.NullValueIgnore)
                            predicateCond = Expression.OrElse(Expression.Equal(propertyFilter, nullConstant), predicateCond);
                        
                        predicate = predicate is null
                            ? predicateCond
                            : Expression.OrElse(predicate, predicateCond);
                    }

                    exp = exp is null
                        ? predicate!
                        : Expression.AndAlso(exp, predicate!);
                }
            }

            exp ??= Expression.Constant(false);

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(exp, itemEntity));
        }

        internal static IOrderableQueryChain<TEntity> SortingGenerate<TEntity, TSortingModel>(IFilterableQueryChain<TEntity> query, Type modelType, IEnumerable<TSortingModel?>? sorting)
            where TSortingModel : QCSorting
        {
            IOrderableQueryChain<TEntity> orderQuery = query.OrderBy(x => null!);

            if (sorting is null || modelType is null) return orderQuery;

            sorting = sorting.Where(x => x is not null).DistinctBy(x => x!.Property);

            if (!sorting.Any()) return orderQuery;

            var properties = modelType
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(QCSortingAttribute)))
                .Where(x => sorting.Any(y => y!.Property.ToLower() == x.Name.ToLower()))
                .ToDictionary(x => x.Name, y => (QCSortingAttribute?)y.GetCustomAttribute(typeof(QCSortingAttribute), false));

            if (properties is null || properties.Count() == 0 || properties.All(x => x.Value is null)) return orderQuery;

            IOrderableQueryChain<TEntity>? resultQuery = null;

            foreach (var prop in properties)
            {
                if (prop.Value is null) continue;

                var sort = sorting.First(x => x!.Property.ToLower() == prop.Key.ToLower());

                var itemEntity = Expression.Parameter(typeof(TEntity), "entity");

                var propertyPath = (prop.Value.FieldName ?? prop.Key).Split(".");

                Expression propertyEntity = itemEntity;
                foreach (var property in propertyPath)
                    propertyEntity = Expression.Property(propertyEntity, property);

                var conversion = Expression.Convert(propertyEntity, typeof(object));
                var predicate = Expression.Lambda<Func<TEntity, object>>(conversion, itemEntity);

                if (resultQuery is null)
                    resultQuery = sort is { Ordering: QCSortingType.Ascending }
                        ? orderQuery.OrderBy(predicate)
                        : orderQuery.OrderByDescending(predicate);
                else
                    resultQuery = sort is { Ordering: QCSortingType.Ascending }
                        ? resultQuery.ThenBy(predicate)
                        : resultQuery.ThenByDescending(predicate);
            }

            return resultQuery ?? orderQuery;
        }

        internal static IFilterableQueryChain<TEntity> PaginateGenerate<TEntity, TPaginateModel>(IFilterableQueryChain<TEntity> query, TPaginateModel? model)
            where TPaginateModel : QCPaginate
        {
            if (model is null)
                return query;

            return query.Skip(model.PageSize * model.Page - model.PageSize).Take(model.PageSize);
        }

        private static Dictionary<string, List<FilterModel>>? ReadFilterModel<TFilterModel>(TFilterModel filter, string name = "default")
        {
            Dictionary<string, List<FilterModel>>? result = null;

            if (filter is null) return result;

            var properties = filter.GetType().GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(QCFilterAttribute)));

            if (properties is null || !properties.Any()) return result;

            var filters = properties
                .SelectMany(x => x.GetCustomAttributes(typeof(QCFilterAttribute), false).Cast<QCFilterAttribute>())
                .Where(x => x.Name == name)
                .Select(x => x.Name)
                .Distinct()
                .ToDictionary(x => x, y => new List<FilterModel>());

            var keys = filters.Keys.ToList();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(QCFilterAttribute), false)
                    .Cast<QCFilterAttribute>()
                    .Where(x => keys.Contains(x.Name))
                    .ToList();

                if (attributes is null || attributes.Count == 0) continue;

                var groups = attributes.GroupBy(x => x.Name).ToDictionary(x => x.Key, y => y.First());

                if (groups is null || groups.Count == 0) continue;

                foreach (var group in groups)
                {
                    if (!filters.ContainsKey(group.Key)) continue;

                    var flt = filters[group.Key];

                    flt.Add(new FilterModel
                    {
                        SrcFieldName = property.Name,
                        DestFieldName = group.Value.FieldName ?? property.Name,

                        Settings = new()
                        {
                            NullValueIgnore = group.Value.NullValueIgnore,
                            StringSensitiveCase = group.Value.StringSensitiveCase
                        },

                        ConditionTypes = group.Value.ConditionTypes
                    });
                }
            }

            result = filters;

            return result;
        }
    }
}
