using DALQueryChain.Interfaces;
using LinqToDB.Data;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        #region Events
        #region Bulk Operations
        protected internal virtual async Task OnBeforeBulkInsertAsync(IEnumerable<TEntity> models, CancellationToken ctn = default)
        {
            foreach (var model in models)
            {
                if (ctn.IsCancellationRequested) break;
                await OnBeforeInsertAsync(model, ctn);
            }
        }

        protected internal virtual async Task OnAfterBulkInsertAsync(IEnumerable<TEntity> models, CancellationToken ctn = default)
        {
            foreach (var model in models)
            {
                if (ctn.IsCancellationRequested) break;
                await OnAfterInsertAsync(model, ctn);
            }
        }

        protected internal virtual async Task OnBeforeBulkUpdateAsync(IEnumerable<TEntity> models, CancellationToken ctn = default)
        {
            foreach (var model in models)
            {
                if (ctn.IsCancellationRequested) break;
                await OnBeforeUpdateAsync(model, ctn);
            }
        }

        protected internal virtual async Task OnAfterBulkUpdateAsync(IEnumerable<TEntity> models, CancellationToken ctn = default)
        {
            foreach (var model in models)
            {
                if (ctn.IsCancellationRequested) break;
                await OnAfterUpdateAsync(model, ctn);
            }
        }

        protected internal virtual async Task OnBeforeBulkDeleteAsync(IEnumerable<TEntity> models, CancellationToken ctn = default)
        {
            foreach (var model in models)
            {
                if (ctn.IsCancellationRequested) break;
                await OnBeforeDeleteAsync(model, ctn);
            }
        }

        protected internal virtual async Task OnAfterBulkDeleteAsync(IEnumerable<TEntity> models, CancellationToken ctn = default)
        {
            foreach (var model in models)
            {
                if (ctn.IsCancellationRequested) break;
                await OnAfterDeleteAsync(model, ctn);
            }
        }
        #endregion

        #region Single Operations
        protected internal virtual Task OnBeforeInsertAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterInsertAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeUpdateAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterUpdateAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeDeleteAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterDeleteAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }
        #endregion

        #endregion

        protected internal virtual Task SoftDeleteAsync(TEntity model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task SoftBulkDeleteAsync(IEnumerable<TEntity> model, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task SoftBulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default)
        {
            return Task.CompletedTask;
        }
    }
}
