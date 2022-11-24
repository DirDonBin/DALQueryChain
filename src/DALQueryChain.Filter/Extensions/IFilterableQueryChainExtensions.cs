using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Helpers;
using DALQueryChain.Filter.Models;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using GrEmit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Extensions
{
    public static class IFilterableQueryChainExtensions
    {
        public static IFilterableQueryChain<TEntity> Filter<TEntity, TFilterModel>(this IFilterableQueryChain<TEntity> query, TFilterModel model, string name = "default") 
        {
            return GenerateFilter(query, model, name);
        }

        public static IFilterableQueryChain<TEntity> Filter<TEntity, TFilterModel>(this IFilterableQueryChain<TEntity> query, TFilterModel model, object name)
        {
            return GenerateFilter(query, model, name.ToString()!);
        }

        private static IFilterableQueryChain<TEntity> GenerateFilter<TEntity, TFilterModel>(IFilterableQueryChain<TEntity> query, TFilterModel model, string name = "default")
        {
            var filters = InitFilter(model, name);

            var targetProperties = typeof(TEntity).GetProperties();
            var srcProperties = typeof(TFilterModel).GetProperties();

            if (filters is null) return query;

            var method = new DynamicMethod("FilterFuncImpl", // имя метода
                                  typeof(bool),
                                  new[] { typeof(TEntity), typeof(TFilterModel) },
                                  true);

            using var il = new GroboIL(method);

            var ilHelper = new IlFilterHelper(il, typeof(TEntity), typeof(TFilterModel));

            //var boolConvertMethod = typeof(Convert).GetMethod("ToBoolean", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(int) }, null);

            il.Nop();

            ilHelper.GenerateFilters(filters);

            il.MarkLabel(ilHelper.labelMethodEndTrue);
            il.Ldc_I4(1);
            il.Br(ilHelper.labelMethodEnd);

            il.MarkLabel(ilHelper.labelMethodEndFalse);
            il.Ldc_I4(0);
            il.Br(ilHelper.labelMethodEnd);

            il.MarkLabel(ilHelper.labelMethodEnd);

            //il.Call(boolConvertMethod);
            il.Nop();
            il.Ret();

            //var tmp = il.GetILCode();

            var funcMethod = (Func<TEntity, TFilterModel, bool>)method.CreateDelegate(typeof(Func<TEntity, TFilterModel, bool>));

            return query.Where(x => funcMethod(x, model));
        }

        private static Dictionary<string, List<TestFilterModel>>? InitFilter<TFilterModel>(TFilterModel filter, string name = "default")
        {
            Dictionary<string, List<TestFilterModel>>? result = null;

            var filters = filter!.GetType()
                .GetCustomAttributes(typeof(QCFilterAttribute), false)
                .Cast<QCFilterAttribute>()
                .Where(x => x.Name == name)
                .ToDictionary(x => x.Name, y => new List<TestFilterModel>());

            if (filters is null || filters.Count == 0) return result;

            var properties = filter.GetType().GetProperties();

            if (properties is null || properties.Length == 0) return result;

            var keys = filters.Keys.ToList();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(QCFilterFieldAttribute), false)
                    .Cast<QCFilterFieldAttribute>()
                    .Where(x => keys.Contains(x.Name))
                    .ToList();

                if (attributes is null || attributes.Count == 0) continue;

                var groups = attributes.GroupBy(x => x.Name).ToDictionary(x => x.Key, y => y.First());

                if (groups is null || groups.Count == 0) continue;

                foreach (var group in groups)
                {
                    if (!filters.ContainsKey(group.Key)) continue;

                    var flt = filters[group.Key];

                    flt.Add(new TestFilterModel
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
