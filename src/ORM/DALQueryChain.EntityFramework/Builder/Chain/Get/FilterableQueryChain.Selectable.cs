using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {
        public IFilterableQueryChain<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            return new FilterableQueryChain<TResult>(_prevQuery.Select(selector));
        }

        public IFilterableQueryChain<TResult> Select<TResult>(Expression<Func<T, int, TResult>> selector)
        {
            return new FilterableQueryChain<TResult>(_prevQuery.Select(selector));
        }

        public IFilterableQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector)
        {
            return new FilterableQueryChain<TResult>(_prevQuery.SelectMany(selector));
        }

        public IFilterableQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, int, IEnumerable<TResult>>> selector)
        {
            return new FilterableQueryChain<TResult>(_prevQuery.SelectMany(selector));
        }
    }
}
