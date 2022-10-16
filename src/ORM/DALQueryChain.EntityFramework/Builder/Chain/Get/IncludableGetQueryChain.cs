using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.EntityFramework.Builder.Chain.Get
{
    internal class IncludableGetQueryChain<T, TPreviousProperty> : FilterableQueryChain<T>, IIncludableGetQueryChain<T, TPreviousProperty>
        where TPreviousProperty : class
    {
        public IncludableGetQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        IQueryable<T> IFilterableQueryChain<T>.Query => _prevQuery;
    }
}
