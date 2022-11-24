using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Extensions;
using DALQueryChain.Filter.Models;
using GrEmit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Helpers
{
    internal class IlFilterHelper
    {
        #region Static

        private static MethodInfo _containsEnumerableMethod;
        private static MethodInfo _containsStringMethod;

        static IlFilterHelper()
        {
            _containsEnumerableMethod = typeof(Enumerable).GetMethods()
                    .Single(x => x.Name == "Contains" && x.GetParameters().Length == 2);

            _containsStringMethod = typeof(string).GetMethods()
                .Single(x => x.Name == "Contains"
                    && x.GetParameters().Length == 1
                    && x.GetParameters().Any(y => y.ParameterType == typeof(string)));
        }

        #endregion

        private readonly GroboIL _il;

        private readonly GroboIL.Local _sourceModel; // entity
        private readonly GroboIL.Local _targetModel; // filter
        private readonly PropertyInfo[] _sourceProperties;
        private readonly PropertyInfo[] _targetProperties;

        internal readonly GroboIL.Label labelMethodEndTrue;
        internal readonly GroboIL.Label labelMethodEndFalse;
        internal readonly GroboIL.Label labelMethodEnd;

        public IlFilterHelper(GroboIL il, Type sourceType, Type targetType)
        {
            _il = il;

            labelMethodEndTrue = _il.DefineLabel(nameof(labelMethodEndTrue));
            labelMethodEndFalse = _il.DefineLabel(nameof(labelMethodEndFalse));
            labelMethodEnd = _il.DefineLabel(nameof(labelMethodEnd));

            _sourceModel = _il.DeclareLocal(sourceType);
            _targetModel = _il.DeclareLocal(targetType);

            _sourceProperties = sourceType.GetProperties();
            _targetProperties = targetType.GetProperties();

            _il.Ldarg(0);
            _il.Stloc(_sourceModel);

            _il.Ldarg(1);
            _il.Stloc(_targetModel);
        }

        public void GenerateFilters(Dictionary<string, List<TestFilterModel>> filters)
        {
            foreach (var flt in filters)
            {
                var labelNext = _il.DefineLabel($"labelNext_{flt.Key}");
                foreach (var item in flt.Value)
                {
                    GenerateFilter(item, labelNext);
                }

                _il.Br(labelMethodEndFalse);
                _il.MarkLabel(labelNext);
            }
        }

        private void GenerateFilter(TestFilterModel filter, GroboIL.Label labelNext)
        {
            var targetType = _targetProperties.FirstOrDefault(x => x.Name == filter.DestFieldName);
            var srcType = _targetProperties.FirstOrDefault(x => x.Name == filter.SrcFieldName);

            if (targetType is null || srcType is null) throw new Exception("Property not found");

            foreach (var condition in filter.ConditionTypes)
            {
                LoadAndPrepareArgs(filter, srcType.PropertyType, targetType.PropertyType, condition);
                GenerateCondition(condition, labelNext, srcType.PropertyType, targetType.PropertyType);
            }
        }

        private void GenerateCondition(QSFilterConditionType condition, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            switch (condition)
            {
                case QSFilterConditionType.Equals:
                    GenerateEquals(nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.NotEqual:
                    GenerateNotEqual(nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.Less:
                    GenerateLess(nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.LessOrEqual:
                    GenerateLessOrEqual(nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.Greater:
                    GenerateGreater(nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.GreaterOrEqual:
                    GenerateGreaterOrEqual(nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.Contains:
                    GenerateContains(nextLabel, sourceType, targetType);
                    break;
            }
        }

        private void GetField(Type type, string propertyName, GroboIL.Local source)
        {
            var getMethod = type.GetMethod($"get_{propertyName}");

            _il.Ldloc(source);
            _il.Call(getMethod);
        }

        private void LoadAndPrepareArgs(TestFilterModel filter, Type sourceFieldType, Type targetFieldType, QSFilterConditionType condition)
        {
            var sourceUnderType = Nullable.GetUnderlyingType(sourceFieldType);
            var targetUnderType = Nullable.GetUnderlyingType(targetFieldType);
            
            //if (sourceUnderType is not null || sourceUnderType is not null) return;

            if (sourceFieldType == targetFieldType)
            {
                GetField(sourceFieldType, filter.SrcFieldName, _sourceModel);
                GetField(targetFieldType, filter.DestFieldName, _targetModel);

                return;
            }

            if (sourceFieldType == typeof(decimal) || targetFieldType == typeof(decimal))
            {
                var convType = new[] { sourceFieldType, targetFieldType }.Single(x => x != typeof(decimal));
                var method = typeof(decimal).GetMethod("op_Implicit", new[] { convType });

                if (method is null) throw new Exception($"It is not possible to compare types {sourceFieldType.Name} and {targetFieldType.Name}");

                GetField(sourceFieldType, filter.SrcFieldName, _sourceModel);
                if (sourceFieldType != typeof(decimal)) _il.Call(method);

                GetField(targetFieldType, filter.DestFieldName, _targetModel);
                if (targetFieldType != typeof(decimal)) _il.Call(method);

                return;
            }

            if (sourceFieldType.IsNumericType() && targetFieldType.IsNumericType())
            {
                var sourceTypeCode = Type.GetTypeCode(sourceFieldType);
                var targetTypeCode = Type.GetTypeCode(targetFieldType);

                GetField(sourceFieldType, filter.SrcFieldName, _sourceModel);

                if (sourceFieldType != targetFieldType && sourceTypeCode > targetTypeCode)
                    typeof(GroboIL)
                        .GetMethod(nameof(GroboIL.Conv))!
                        .MakeGenericMethod(sourceFieldType)
                        .Invoke(null, null);

                GetField(targetFieldType, filter.DestFieldName, _targetModel);

                if (sourceFieldType != targetFieldType && sourceTypeCode < targetTypeCode)
                    typeof(GroboIL)
                        .GetMethod(nameof(GroboIL.Conv))!
                        .MakeGenericMethod(targetFieldType)
                        .Invoke(null, null);

                return;
            }

            if (condition == QSFilterConditionType.Contains && targetFieldType == typeof(string) && sourceFieldType == typeof(string))
            {
                GetField(targetFieldType, filter.DestFieldName, _targetModel);
                GetField(sourceFieldType, filter.SrcFieldName, _sourceModel);

                return;
            }

            if (condition == QSFilterConditionType.Contains && targetFieldType.IsAssignableTo(typeof(Enumerable)))
            {
                GetField(sourceFieldType, filter.SrcFieldName, _sourceModel);
                GetField(targetFieldType, filter.DestFieldName, _targetModel);

                return;
            }

            throw new Exception($"It is not possible to compare types {sourceFieldType.Name} and {targetFieldType.Name}");
        }


        private (Type sourceType, Type targetType)? GenerateNullableCondition(Type sourceType, Type targetType)
        {
            (Type sourceType, Type targetType)? resultTypes = null;

            var sourceUnderType = Nullable.GetUnderlyingType(sourceType);
            var targetUnderType = Nullable.GetUnderlyingType(targetType);
            if (sourceUnderType is null && targetUnderType is null) return resultTypes;

            (MethodInfo get, MethodInfo has)? sourceMethods = sourceType is not null 
                ? (sourceType.GetMethod("GetValueOrDefault")!, sourceType.GetMethod("get_HasValue")!)
                : null;

            (MethodInfo get, MethodInfo has)? targetMethods = targetType is not null 
                ? (targetType.GetMethod("GetValueOrDefault")!, targetType.GetMethod("get_HasValue")!)
                : null;

            if (targetType is not null)
                targetMethods = ;

            if (sourceMethods is not null)
            {

            }

            return resultTypes;
        }
    }
}
