using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface ISelectableQueryChain<T>
    {
        IFilterableQueryChain<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);
        IFilterableQueryChain<TResult> Select<TResult>(Expression<Func<T, int, TResult>> selector);
        IFilterableQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector);
        IFilterableQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, int, IEnumerable<TResult>>> selector);
    }
}
