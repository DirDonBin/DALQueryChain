using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace DALQueryChain.EntityFramework.Extensions
{
    public static class EFCoreQueryableExtensions
    {
        private static readonly MethodInfo _asQueryableMethodInfo
            = typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(m => m.Name == nameof(Queryable.AsQueryable) && m.IsGenericMethod);

        public static IQueryable<TEntity> AsSubQuery<TEntity>(this IQueryable<TEntity> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Provider is not EntityQueryProvider)
                return source;

            var methodCall = Expression.Call(
                                    null,
                                    _asQueryableMethodInfo.MakeGenericMethod(typeof(TEntity)),
                                    source.Expression);

            return source.Provider.CreateQuery<TEntity>(methodCall);
        }
    }
}
