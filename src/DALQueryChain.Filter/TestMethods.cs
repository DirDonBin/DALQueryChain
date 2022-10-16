using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using GrEmit;

namespace DALQueryChain.Filter
{
    public static class TestMethods
    {

        public interface IQCFilter
        {
            public Func<TestModel, bool> Test();
        }

        public static List<TestModel> FilterTestModels(List<TestModel> data, TestModel filter)
        {
            return TestGenerate(data, filter);
        }

        private static List<TestModel> TestGenerate(List<TestModel> data, TestModel filter)
        {
            var result = new List<TestModel>();

            var filters = InitFilter(filter);

            if (filters is null) return result;

            var method = new DynamicMethod("FilterImpl", // имя метода
                                  typeof(void), // возвращаемый тип
                                  new[] { typeof(TestModel), typeof(TestModel) }, // принимаемые параметры
                                  true); // просим доступ к приватным полям

            using var il = new GroboIL(method);

            var getField1 = typeof(TestModel).GetMethod($"get_Id");

            il.Ldarg(1);
            il.Call(getField1!);

            il.Ldarg(0);
            il.Call(getField1!);


            il.Ret();

            var del = (Action<TestModel, TestModel>)method.CreateDelegate(typeof(Action<TestModel, TestModel>));
            del.Invoke(data[0], filter);
            return result;
        }

        private static Dictionary<string, List<TestFilterModel>>? InitFilter(TestModel filter)
        {
            Dictionary<string, List<TestFilterModel>>? result = null;

            var filters = filter.GetType()
                .GetCustomAttributes(typeof(QCFilterAttribute), false)
                .Cast<QCFilterAttribute>()
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
                        FieldName = group.Value.FieldName ?? property.Name,
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
        public string? FieldName { get; set; }
        public QSFilterConditionType[] ConditionTypes { get; set; } = null!;
    }
}
