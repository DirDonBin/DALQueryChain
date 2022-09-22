using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain.Get
{
    internal class OrderableQueryChain<T> : FilterableQueryChain<T>, IOrderableQueryChain<T>
    {
        public OrderableQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IOrderableQueryChain<T> ThenBy(Expression<Func<T, object>> selector)
        {
            return new OrderableQueryChain<T>(((IOrderedQueryable<T>)_prevQuery).ThenBy(selector));
        }

        public IOrderableQueryChain<T> ThenByDescending(Expression<Func<T, object>> selector)
        {
            return new OrderableQueryChain<T>(((IOrderedQueryable<T>)_prevQuery).ThenByDescending(selector));
        }
    }
}
