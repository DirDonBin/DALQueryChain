using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB.Data;
using System.Collections.Concurrent;

namespace DALQueryChain.Linq2Db.Builder
{
    /// <summary>
    /// Helper for building queries to database
    /// </summary>
    /// <typeparam name="TContext">Data Connection</typeparam>
    public class BuildQuery<TContext> : IDALQueryChain<TContext>
        where TContext : notnull, DataConnection
    {
        private readonly TContext _context;
        private ConcurrentDictionary<Type, object> _cacheQBC;

        public BuildQuery(TContext context)
        {
            _context = context;
            _cacheQBC = new();
        }

        /// <summary>
        /// Create query for specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        public IQueryBuilder<TEntity> For<TEntity>() where TEntity : class, IDbModelBase
        {
            var qbc = _cacheQBC.GetOrAdd(typeof(TEntity), new QueryBuilderChain<TContext, TEntity>(_context, this));

            return (IQueryBuilder<TEntity>)qbc;
        }
    }
}
