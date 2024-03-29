﻿using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Triggres;
using LinqToDB.Data;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : BaseTrigger<TContext, TEntity>, IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        protected readonly TContext _context;
        private IDALQueryChain<TContext>? _dalQueryChain;

        protected IQueryBuilder<TEntity> QueryChain => _dalQueryChain!.For<TEntity>();
        protected TRepository GetRepository<TRepository>() => _dalQueryChain!.Repository<TRepository>();

        public BaseRepository(TContext context) : base(context)
        {
            _context = context;
        }

        internal void InitQueryChain(IDALQueryChain<TContext> dalQueryChain)
        {
            _dalQueryChain = dalQueryChain;
        }

        protected IQueryBuilder<T> GetQueryChain<T>() where T : class, IDbModelBase => _dalQueryChain!.For<T>();

        #region Soft Delete

        protected internal virtual Task SoftDelete(TEntity model, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftDelete(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftBulkDelete(IEnumerable<TEntity> model, CancellationToken ctn = default) => Task.CompletedTask;
        protected internal virtual Task SoftBulkDelete(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => Task.CompletedTask;

        #endregion
    }
}
