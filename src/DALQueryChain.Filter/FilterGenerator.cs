using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Extensions;
using DALQueryChain.Filter.Models;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace DALQueryChain.Filter
{
    internal static class FilterGenerator
    {
        internal static IFilterableQueryChain<TEntity> Generate<TEntity, TFilterModel>(IFilterableQueryChain<TEntity> query, TFilterModel model, string name = "default")
        {
            var filters = ReadFilterModel(model, name);

            if (filters is null || filters.Count == 0) return query;

            Expression exp = default!;

            var itemEntity = Expression.Parameter(typeof(TEntity), "entity");
            var itemFilter = Expression.Constant(model);

            foreach (var filter in filters)
            {
                if (filter.Value.Count == 0) continue;

                foreach (var flt in filter.Value)
                {
                    Expression? predicate = null;

                    Expression propertyEntity = Expression.Property(itemEntity, flt.DestFieldName);
                    Expression propertyFilter = Expression.Property(itemFilter, flt.SrcFieldName);

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
                            var invariant = Expression.Constant(StringComparison.InvariantCultureIgnoreCase);
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

                    foreach (var condition in flt.ConditionTypes)
                    {
                        var predicateCond = condition switch
                        {
                            QSFilterCondition.Equals => Expression.Equal(propertyFilter, propertyEntity),
                            QSFilterCondition.NotEqual => Expression.NotEqual(propertyFilter, propertyEntity),
                            QSFilterCondition.Greater => Expression.GreaterThan(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QSFilterCondition.GreaterOrEqual => Expression.GreaterThanOrEqual(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QSFilterCondition.Less => Expression.LessThan(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QSFilterCondition.LessOrEqual => Expression.LessThanOrEqual(Expression.Convert(propertyFilter, propertyEntity.Type), propertyEntity),
                            QSFilterCondition.Contains => contains(propertyEntity, propertyFilter),
                            _ => throw new Exception()
                        };

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

                        ConditionTypes = group.Value.ConditionTypes
                    });
                }
            }

            result = filters;

            return result;
        }
    }
}
