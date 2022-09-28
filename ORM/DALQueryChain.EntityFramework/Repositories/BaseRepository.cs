using DALQueryChain.EntityFramework.Builder;
using DALQueryChain.EntityFramework.Triggres;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : BaseTrigger<TContext, TEntity>, IRepository
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        private readonly TContext _context;
        private IDALQueryChain<TContext>? _dalQueryChain;

        protected readonly IQueryable<TEntity> _query;
        protected IQueryBuilder<TEntity> _queryChain => _dalQueryChain!.For<TEntity>();

        public BaseRepository(TContext context) : base(context)
        {
            _context = context;
            _query = context.Set<TEntity>().AsQueryable();
        }

        internal void InitQueryChain(IDALQueryChain<TContext>? dalQueryChain, IServiceProvider serviceProvider)
        {
            _dalQueryChain = dalQueryChain ??= new BuildQuery<TContext>(_context, serviceProvider);
        }

        protected IQueryBuilder<T> GetQueryChain<T>() where T : class, IDbModelBase => _dalQueryChain!.For<T>();

        protected IQueryable<T> GetQuery<T>() where T : class, IDbModelBase => _context.Set<T>().AsQueryable();

        #region Soft Delete

        protected internal virtual Task SoftDelete(TEntity model, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftDelete(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftBulkDelete(IEnumerable<TEntity> model, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftBulkDelete(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => Task.CompletedTask;

        #endregion
    }
}
