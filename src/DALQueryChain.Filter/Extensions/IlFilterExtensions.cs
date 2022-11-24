using DALQueryChain.Filter.Enums;
using GrEmit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Extensions
{
    internal static class IlFilterExtensions
    {
        private static MethodInfo _containsEnumerableMethod;
        private static MethodInfo _containsStringMethod;

        static IlFilterExtensions()
        {
            _containsEnumerableMethod = typeof(Enumerable).GetMethods()
                .Single(x => x.Name == "Contains" && x.GetParameters().Length == 2);

            _containsStringMethod = typeof(string).GetMethods()
                .Single(x => x.Name == "Contains"
                    && x.GetParameters().Length == 1
                    && x.GetParameters().Any(y => y.ParameterType == typeof(string)));
        }

        internal static void GenerateCondition(this QSFilterConditionType type, GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            switch (type)
            {
                case QSFilterConditionType.Equals:
                    GenerateEquals(il, nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.NotEqual:
                    GenerateNotEqual(il, nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.Less:
                    GenerateLess(il, nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.LessOrEqual:
                    GenerateLessOrEqual(il, nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.Greater:
                    GenerateGreater(il, nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.GreaterOrEqual:
                    GenerateGreaterOrEqual(il, nextLabel, sourceType, targetType);
                    break;
                case QSFilterConditionType.Contains:
                    GenerateContains(il, nextLabel, sourceType, targetType);
                    break;
            }
        }

        #region Generate Concrete Condition

        public static bool GenerateMethodCondition(GroboIL il, Type propertyType, string methodName)
        {
            if (propertyType.IsNumericType()) return false;

            var method = propertyType.GetMethod(methodName);

            if (method == null) return false;

            il.Call(method);

            return true;
        }

        private static void GenerateEquals(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            if (!GenerateMethodCondition(il, sourceType, "op_Equality")) il.Ceq();

            il.Brtrue(nextLabel);
        }

        private static void GenerateNotEqual(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            if (!GenerateMethodCondition(il, sourceType, "op_Equality"))
            {
                il.Ceq();
                il.Brfalse(nextLabel);
            }
            else il.Brtrue(nextLabel);
        }

        private static void GenerateLess(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            if (!GenerateMethodCondition(il, sourceType, "op_LessThan"))
            {
                var unsigned = Type.GetTypeCode(sourceType) > Type.GetTypeCode(targetType)
                    ? sourceType.IsUnsignedNumericType()
                    : targetType.IsUnsignedNumericType();

                il.Clt(unsigned);
            }

            il.Brtrue(nextLabel);
        }

        private static void GenerateLessOrEqual(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            if (!GenerateMethodCondition(il, sourceType, "op_LessThanOrEqual"))
            {
                var unsigned = Type.GetTypeCode(sourceType) > Type.GetTypeCode(targetType)
                    ? sourceType.IsUnsignedNumericType()
                    : targetType.IsUnsignedNumericType();

                il.Cgt(unsigned);

                il.Brfalse(nextLabel);
            }
            else il.Brtrue(nextLabel);
        }

        private static void GenerateGreater(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            if (!GenerateMethodCondition(il, sourceType, "op_GreaterThan"))
            {
                var unsigned = Type.GetTypeCode(sourceType) > Type.GetTypeCode(targetType)
                    ? sourceType.IsUnsignedNumericType()
                    : targetType.IsUnsignedNumericType();

                il.Cgt(unsigned);
            }

            il.Brtrue(nextLabel);
        }

        private static void GenerateGreaterOrEqual(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            if (!GenerateMethodCondition(il, sourceType, "op_GreaterThanOrEqual"))
            {
                var unsigned = Type.GetTypeCode(sourceType) > Type.GetTypeCode(targetType)
                    ? sourceType.IsUnsignedNumericType()
                    : targetType.IsUnsignedNumericType();

                il.Clt(unsigned);

                il.Brfalse(nextLabel);
            }
            else il.Brtrue(nextLabel);
        }

        private static void GenerateContains(GroboIL il, GroboIL.Label nextLabel, Type sourceType, Type targetType)
        {
            var method = sourceType == typeof(string)
                ? _containsStringMethod
                : _containsEnumerableMethod!.MakeGenericMethod(targetType);

            il.Call(method);
            il.Brtrue(nextLabel);
        }

        #endregion
    }
}
