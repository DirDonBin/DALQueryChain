using DALQueryChain.Interfaces.QueryBuilder.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain.Get
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
