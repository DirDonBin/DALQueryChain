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

        public IIncludableGetQueryChain<T, IEnumerable<TProperty>> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> selector)
        {
            return new IncludableGetQueryChain<T, IEnumerable<TProperty>>(((ILoadWithQueryable<T, TPreviousProperty>)_prevQuery).ThenLoad(selector));
        }

        public IIncludableGetQueryChain<T, TProperty> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, TProperty>> selector)
        {
            return new IncludableGetQueryChain<T, TProperty>(((ILoadWithQueryable<T, TPreviousProperty>)_prevQuery).ThenLoad(selector));
        }
    }
}
