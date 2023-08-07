using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain;

namespace DALQueryChain.Linq2Db.Extensions
{
    public static class FilterableExtensions
    {
        public static IFilterableQueryChain<T> AsQueryChain<T>(this IQueryable<T> query)
            => new FilterableQueryChain<T>(query);
    }
}
