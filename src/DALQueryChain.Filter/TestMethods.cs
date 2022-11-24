using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Models;
using System.Reflection;
using System.Reflection.Emit;
using GrEmit;
using System.Diagnostics.SymbolStore;
using System.Security.Permissions;
using System.Linq.Expressions;
using System;
using System.Resources;

namespace DALQueryChain.Filter
{
    public static class TestMethods
    {

        public interface IQCFilter
        {
            public Func<TestModel, bool> Test();
        }

        //public static List<TModel> FilterTestModels<TModel>(List<TModel> data, TestModel filter, string name = "default")
        //{
        //    return TestGenerate(data, filter, name);
        //}

        //private static List<TModel> TestGenerate<TModel>(List<TModel> data, TestModel filter, string name = "default")
        //{
        //    var result = new List<TModel>();

        //    var filters = InitFilter(filter, name);
        //    var targetProperties = typeof(TModel).GetProperties();
        //    var srcProperties = filter.GetType().GetProperties();

        //    if (filters is null) return result;

        //    var method = new DynamicMethod("FilterFuncImpl", // имя метода
        //                          typeof(bool),
        //                          new[] { typeof(TModel), typeof(TestModel) },
        //                          true);

        //    using var il = new GroboIL(method);

        //    var boolConvertMethod = typeof(Convert).GetMethod("ToBoolean", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(int) }, null);



        //    void GetFields(string srcName, string destName)
        //    {
        //        var getFieldSrc = typeof(TestModel).GetMethod($"get_{srcName}");
        //        var getFieldDest = typeof(TModel).GetMethod($"get_{destName}");

        //        il.Ldarg(1);
        //        il.Call(getFieldSrc!);

        //        il.Ldarg(0);
        //        il.Call(getFieldDest!);
        //    }

        //    il.Nop();

        //    var labelMethodEndTrue = il.DefineLabel("labelMethodEndTrue");
        //    var labelMethodEndFalse = il.DefineLabel("labelMethodEndFalse");
        //    var labelMethodEnd = il.DefineLabel("labelMethodEnd");

        //    foreach (var flt in filters)
        //    {
        //        var labelNext = il.DefineLabel($"labelNext_{flt.Key}");
        //        foreach (var item in flt.Value)
        //        {
        //            foreach (var condition in item.ConditionTypes)
        //            {
        //                GetFields(item.SrcFieldName, item.DestFieldName);
        //                var targetType = targetProperties.FirstOrDefault(x => x.Name == item.DestFieldName);
        //                var srcType = targetProperties.FirstOrDefault(x => x.Name == item.SrcFieldName);

        //                if (targetType is null || srcType is null) throw new Exception("property not found");

        //                condition.GenerateCondition(il, labelNext, srcType.PropertyType, targetType.PropertyType);
        //            }
        //        }

        //        il.Br(labelMethodEndFalse);
        //        il.MarkLabel(labelNext);
        //    }

        //    il.MarkLabel(labelMethodEndTrue);
        //    il.Ldc_I4(1);
        //    il.Br(labelMethodEnd);

        //    il.MarkLabel(labelMethodEndFalse);
        //    il.Ldc_I4(0);
        //    il.Br(labelMethodEnd);

        //    il.MarkLabel(labelMethodEnd);

        //    il.Call(boolConvertMethod);
        //    il.Nop();
        //    il.Ret();

        //    var tmp = il.GetILCode();

        //    var funcMethod = (Func<TModel, TestModel, bool>)method.CreateDelegate(typeof(Func<TModel, TestModel, bool>));

        //    //var tmp2 = funcMethod(data[4], filter);
        //    //var tmp3 = funcMethod(data[5], filter);
        //    //var tmp4 = funcMethod(data[0], filter);
        //    //var tmp5 = funcMethod(data[9], filter);

        //    result = data.Where(x => funcMethod(x, filter)).ToList();

        //    //funcMethod.Invoke(data[0], filter);
        //    return result;
        //}

        private static Dictionary<string, List<TestFilterModel>>? InitFilter(TestModel filter, string name = "default")
        {
            Dictionary<string, List<TestFilterModel>>? result = null;

            var filters = filter.GetType()
                .GetCustomAttributes(typeof(QCFilterAttribute), false)
                .Cast<QCFilterAttribute>()
                .Where(x => x.Name == name)
                .ToDictionary(x => x.Name, y => new List<TestFilterModel>());

            if (filters is null || filters.Count == 0) return result;

            var properties = filter.GetType().GetProperties();

            if (properties is null || properties.Count() == 0) return result;

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

    internal class TestFilterModel
    {
        public string SrcFieldName { get; set; } = null!;
        public string DestFieldName { get; set; } = null!;
        public QSFilterConditionType[] ConditionTypes { get; set; } = null!;
    }
}
