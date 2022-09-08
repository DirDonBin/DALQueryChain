using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.EntityFramework.Builder.Chain.Get
{
    internal class IncludableGetQueryChain<T, TPreviousProperty> : FilterableQueryChain<T>, IIncludableGetQueryChain<T, TPreviousProperty>
        where T : class
    {
        public IncludableGetQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        IQueryable<T> IIncludableGetQueryChain<T, TPreviousProperty>.Query => _prevQuery;
    }
}
