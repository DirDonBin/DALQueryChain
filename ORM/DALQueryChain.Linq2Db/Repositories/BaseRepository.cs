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
        private readonly IDALQueryChain<TContext> _dalQueryChain;

        protected readonly IQueryable<TEntity> _query;
        protected IQueryBuilder<TEntity> _queryChain => _dalQueryChain.For<TEntity>();

        public BaseRepository(TContext context)
        {
            _context = context;
            _query = context.GetTable<TEntity>().AsQueryable();
            _dalQueryChain = new BuildQuery<TContext>(context);
        }

        protected IQueryBuilder<T> GetQueryChain<T>() where T : class, IDbModelBase => _dalQueryChain.For<T>();

        protected IQueryable<T> GetQuery<T>() where T : class, IDbModelBase => _context.GetTable<T>().AsQueryable();
    }
}
