using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Builder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly TContext _context;

        protected readonly IQueryable<TEntity> _query;
        protected readonly IQueryBuilder<TEntity> _queryChain;

        public BaseRepository(TContext context)
        {
            _context = context;
            _query = context.GetTable<TEntity>().AsQueryable();
            _queryChain = new QueryBuilderChain<TContext, TEntity>(_context);
        }

        protected IQueryBuilder<T> GetQueryChain<T>()
            where T : class, IDbModelBase
        {
            return new QueryBuilderChain<TContext, T>(_context);
        }

        protected IQueryable<T> GetQuery<T>()
            where T : class, IDbModelBase
        {
            return _context.GetTable<T>().AsQueryable();
        }
    }
}
