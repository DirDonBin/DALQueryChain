using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public partial interface IExecutableQueryChain<T>
        where T : class
    {
        /// <summary>
		/// Returns the first element of a sequence, or a default value if the sequence contains no elements
		/// </summary>
		T? FirstOrDefault();

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
		/// Returns the first element of a sequence
		/// </summary>
		T First();

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        T First(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        T? SingleOrDefault();

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists;
        /// this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <param name="predicate">A function to test an element for a condition</param>
        T? SingleOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Creates a System.Collections.Generic.List`1 from an query
        /// </summary>
        List<T> ToList();

        /// <summary>
        /// Returns the number of elements in a sequence
        /// </summary>
        int Count();

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Determines whether a sequence contains any elements
        /// </summary>
        bool Any();

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        bool All(Expression<Func<T, bool>> predicate);
    }
}
