using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IOrderableQueryChain<T> : IFilterableQueryChain<T>
    {
        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key
        /// </summary>
        /// <param name="selector">A function to extract a key from each element</param>
        IOrderableQueryChain<T> ThenBy(Expression<Func<T, object>> selector);

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key
        /// </summary>
        /// <param name="selector">A function to extract a key from each element</param>
        IOrderableQueryChain<T> ThenByDescending(Expression<Func<T, object>> selector);
    }
}
