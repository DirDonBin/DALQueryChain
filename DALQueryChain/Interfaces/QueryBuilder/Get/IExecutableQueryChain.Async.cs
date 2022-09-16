using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public partial interface IExecutableQueryChain<T>
        where T : class
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
    }
}
