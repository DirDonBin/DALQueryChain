using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.EntityFramework.Builder.Chain;

namespace DALQueryChain.EntityFramework.Extensions
{
    public static class FilterableExtensions
    {
        public static IFilterableQueryChain<T> AsQueryChain<T>(this IQueryable<T> query)
            => new FilterableQueryChain<T>(query);
    }
}
