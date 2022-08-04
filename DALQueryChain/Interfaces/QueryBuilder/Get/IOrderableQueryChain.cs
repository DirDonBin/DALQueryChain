using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IOrderableQueryChain<T> : IFilterableQueryChain<T>
        where T : class
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
