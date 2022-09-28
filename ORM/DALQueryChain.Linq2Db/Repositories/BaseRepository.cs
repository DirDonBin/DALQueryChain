using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Builder;
using DALQueryChain.Linq2Db.Triggres;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : BaseTrigger<TContext, TEntity>, IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly TContext _context;
        private IDALQueryChain<TContext>? _dalQueryChain;

        protected readonly IQueryable<TEntity> _query;
        protected IQueryBuilder<TEntity> QueryChain => _dalQueryChain!.For<TEntity>();

        public BaseRepository(TContext context) : base(context)
        {
            _context = context;
            _query = context.GetTable<TEntity>().AsQueryable();
        }

        internal void InitQueryChain(IDALQueryChain<TContext>? dalQueryChain, IServiceProvider serviceProvider)
        {
            _dalQueryChain = dalQueryChain ?? new BuildQuery<TContext>(_context, serviceProvider);
        }

        protected IQueryBuilder<T> GetQueryChain<T>() where T : class, IDbModelBase => _dalQueryChain!.For<T>();

        protected IQueryable<T> GetQuery<T>() where T : class, IDbModelBase => _context.GetTable<T>().AsQueryable();

        #region Soft Delete

        protected internal virtual Task SoftDelete(TEntity model, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftDelete(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftBulkDelete(IEnumerable<TEntity> model, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftBulkDelete(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => Task.CompletedTask;

        #endregion
    }
}
