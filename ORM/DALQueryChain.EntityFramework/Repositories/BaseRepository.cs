using DALQueryChain.EntityFramework.Builder;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : IRepository
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        private readonly TContext _context;
        private IDALQueryChain<TContext>? _dalQueryChain;

        protected readonly IQueryable<TEntity> _query;
        protected IQueryBuilder<TEntity> _queryChain => _dalQueryChain!.For<TEntity>();

        public BaseRepository(TContext context)
        {
            _context = context;
            _query = context.Set<TEntity>().AsQueryable();
        }

        internal void InitQueryChain(IDALQueryChain<TContext>? dalQueryChain)
        {
            _dalQueryChain = dalQueryChain ??= new BuildQuery<TContext>(_context);
        }

        protected IQueryBuilder<T> GetQueryChain<T>() where T : class, IDbModelBase => _dalQueryChain!.For<T>();

        protected IQueryable<T> GetQuery<T>() where T : class, IDbModelBase => _context.Set<T>().AsQueryable();
    }
}
