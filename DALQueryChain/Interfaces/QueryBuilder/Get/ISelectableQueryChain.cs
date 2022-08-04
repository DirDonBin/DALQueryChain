using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface ISelectableQueryChain<T>
        where T : class
    {
        IFilterableQueryChain<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class;
        IFilterableQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector) where TResult : class;
    }
}
