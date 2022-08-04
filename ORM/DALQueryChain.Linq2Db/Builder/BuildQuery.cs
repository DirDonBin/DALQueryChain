using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public BuildQuery(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create query for specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        public IQueryBuilder<TEntity> For<TEntity>() where TEntity : class, IDbModelBase
        {
            return new QueryBuilderChain<TContext, TEntity>(_context);
        }
    }
}
