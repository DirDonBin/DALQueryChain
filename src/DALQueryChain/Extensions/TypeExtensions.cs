using System.Reflection;

namespace DALQueryChain.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType, params Type[]? checkedTypes)
        {

            if (CheckGenericType(givenType, genericType, checkedTypes))
                return true;

            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (CheckGenericType(it, genericType, checkedTypes))
                    return true;
            }

            if (givenType.BaseType == null) return false;

            return IsAssignableToGenericType(givenType.BaseType, genericType, checkedTypes);
        }

        public static MethodInfo? GetMethodByAllParrent(this Type givenType, string name, BindingFlags flags)
        {
            var method = givenType.GetMethod(name, flags);

            if (method is not null) return method;

            if (givenType.BaseType is null) return null;

            return GetMethodByAllParrent(givenType.BaseType, name, flags);
        }

        private static bool CheckGenericType(Type givenType, Type genericType, Type[]? checkedTypes)
        => givenType.IsGenericType
            && givenType.GetGenericTypeDefinition() == genericType
            && (checkedTypes == null
                || checkedTypes.Length == 0
                || checkedTypes.All(y => givenType.GenericTypeArguments.Contains(y)));
    }
}
