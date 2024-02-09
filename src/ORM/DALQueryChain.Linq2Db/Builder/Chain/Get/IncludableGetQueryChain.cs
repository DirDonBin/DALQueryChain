using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.Linq2Db.Builder.Chain.Get
{
    internal class IncludableGetQueryChain<T, TPreviousProperty> : FilterableQueryChain<T>, IIncludableGetQueryChain<T, TPreviousProperty>
        where TPreviousProperty : class
    {
        internal IFilterableQueryChain<TPreviousProperty> QueryPreviousProperty;
        public IncludableGetQueryChain(IQueryable<T> prevQuery, IFilterableQueryChain<TPreviousProperty> queryPreviousProperty) : base(prevQuery)
        {
            QueryPreviousProperty = queryPreviousProperty;
        }
    }
}
