using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public partial interface IExecutableQueryChain<T>
    {
        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        Task<T?> FirstOrDefaultAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the first element of a sequence
        /// </summary>
        Task<T> FirstAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        Task<T?> SingleOrDefaultAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists;
        /// this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <param name="predicate">A function to test an element for a condition</param>
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        /// <returns></returns>
        Task<T?> LastOrDefaultAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element of the sequence that satisfies a condition or a default value if no such element is found
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns></returns>
        Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element according to a specified key selector function or a default value if no such element is found
        /// </summary>
        /// <param name="keySelector">A function to extract the key for each element</param>
        /// <returns></returns>
        Task<T?> LastOrDefaultAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element of the sequence that satisfies a condition and according to a specified key selector function or a default value if no such element is found
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="keySelector">A function to extract the key for each element</param>
        /// <returns></returns>
        Task<T?> LastOrDefaultAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element of a sequence
        /// </summary>
        /// <returns></returns>
        Task<T> LastAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element of the sequence that satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns></returns>
        Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element according to a specified key selector function
        /// </summary>
        /// <param name="keySelector">A function to extract the key for each element</param>
        /// <returns></returns>
        Task<T> LastAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default);

        /// <summary>
        /// Returns the last element of the sequence that satisfies a condition and according to a specified key selector function
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="keySelector">A function to extract the key for each element</param>
        /// <returns></returns>
        Task<T> LastAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default);

        /// <summary>
        /// Creates a System.Collections.Generic.List`1 from an query
        /// </summary>
        Task<List<T>> ToListAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the number of elements in a sequence
        /// </summary>
        Task<int> CountAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Determines whether a sequence contains any elements
        /// </summary>
        Task<bool> AnyAsync(CancellationToken ctn = default);

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the maximum value
        /// </summary>
        /// <returns></returns>
        Task<T?> MaxAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the maximum value of a sequence satisfy a condition 
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns></returns>
        Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the maximum value according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
        /// <param name="keySelector">A function to extract the key for each element</param>
        /// <returns></returns>
        Task<T?> MaxByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default);

        /// <summary>
        /// Returns the minimum value
        /// </summary>
        /// <returns></returns>
        Task<T?> MinAsync(CancellationToken ctn = default);

        /// <summary>
        /// Returns the minimum value of a sequence satisfy a condition 
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns></returns>
        Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default);

        /// <summary>
        /// Returns the minimum value according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
        /// <param name="keySelector">A function to extract the key for each element</param>
        /// <returns></returns>
        Task<T?> MinByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default);
    }
}
