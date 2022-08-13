using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
        where T : class
    {
        public IFilterableQueryChain<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class
        {
            return new FilterableQueryChain<TResult>(_prevQuery.Select(selector));
        }

        public IFilterableQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector) where TResult : class
        {
            return new FilterableQueryChain<TResult>(_prevQuery.SelectMany(selector));
        }
    }
}
