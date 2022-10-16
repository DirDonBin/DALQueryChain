using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.Linq2Db.Builder.Chain.Get
{
    internal class IncludableGetQueryChain<T, TPreviousProperty> : FilterableQueryChain<T>, IIncludableGetQueryChain<T, TPreviousProperty>
        where TPreviousProperty : class
    {
        public IncludableGetQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }
    }
}
