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
        private IDALQueryChain<TContext>? _dalQueryChain;

        protected readonly IQueryable<TEntity> _query;
        protected IQueryBuilder<TEntity> QueryChain => _dalQueryChain!.For<TEntity>();

        public BaseRepository(TContext context)
        {
            _context = context;
            _query = context.GetTable<TEntity>().AsQueryable();

        }

        internal void InitQueryChain(IDALQueryChain<TContext>? dalQueryChain)
        {
            _dalQueryChain = dalQueryChain ?? new BuildQuery<TContext>(_context);
        }

        protected IQueryBuilder<T> GetQueryChain<T>() where T : class, IDbModelBase => _dalQueryChain!.For<T>();

        protected IQueryable<T> GetQuery<T>() where T : class, IDbModelBase => _context.GetTable<T>().AsQueryable();
    }
}
