using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            if (givenType.BaseType == null) return false;

            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        public static MethodInfo? GetMethodByAllParrent(this Type givenType, string name, BindingFlags flags)
        {
            var method = givenType.GetMethod(name, flags);

            if (method is not null) return method;

            if (givenType.BaseType is null) return null;

            return GetMethodByAllParrent(givenType.BaseType, name, flags);
        }
    }
}
