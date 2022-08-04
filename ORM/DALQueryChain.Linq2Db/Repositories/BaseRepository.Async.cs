using DALQueryChain.Interfaces;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        #region Events
        #region Bulk Operations
        protected internal virtual async Task OnBeforeBulkInsertAsync(IEnumerable<TEntity> models)
        {
            foreach (var model in models)
                await OnBeforeInsertAsync(model);
        }

        protected internal virtual async Task OnAfterBulkInsertAsync(IEnumerable<TEntity> models)
        {
            foreach (var model in models)
                await OnAfterInsertAsync(model);
        }

        protected internal virtual async Task OnBeforeBulkUpdateAsync(IEnumerable<TEntity> models)
        {
            foreach (var model in models)
                await OnBeforeUpdateAsync(model);
        }

        protected internal virtual async Task OnAfterBulkUpdateAsync(IEnumerable<TEntity> models)
        {
            foreach (var model in models)
                await OnAfterUpdateAsync(model);
        }

        protected internal virtual async Task OnBeforeBulkDeleteAsync(IEnumerable<TEntity> models)
        {
            foreach (var model in models)
                await OnBeforeDeleteAsync(model);
        }

        protected internal virtual async Task OnAfterBulkDeleteAsync(IEnumerable<TEntity> models)
        {
            foreach (var model in models)
                await OnAfterDeleteAsync(model);
        }
        #endregion

        #region Single Operations
        protected internal virtual Task OnBeforeInsertAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterInsertAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeUpdateAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterUpdateAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeDeleteAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterDeleteAsync(TEntity model)
        {
            return Task.CompletedTask;
        } 
        #endregion

        #endregion

        protected internal virtual Task SoftDeleteAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task SoftBulkDeleteAsync(IEnumerable<TEntity> model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task SoftBulkDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.CompletedTask;
        }
    }
}
