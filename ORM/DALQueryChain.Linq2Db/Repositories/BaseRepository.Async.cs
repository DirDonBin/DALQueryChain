using DALQueryChain.Interfaces;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        protected internal virtual Task OnBeforeBulkInsertAsync(IEnumerable<TEntity> models)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterBulkInsertAsync(IEnumerable<TEntity> models)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeInsertAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterInsertAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeBulkUpdateAsync(IEnumerable<TEntity> models)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterBulkUpdateAsync(IEnumerable<TEntity> models)
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

        protected internal virtual Task OnBeforeBulkDeleteAsync(IEnumerable<TEntity> models)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterBulkDeleteAsync(IEnumerable<TEntity> models)
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

        protected internal virtual Task OnBeforeBulkSoftDeleteAsync(IEnumerable<TEntity> models)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterBulkSoftDeleteAsync(IEnumerable<TEntity> models)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnBeforeSoftDeleteAsync(TEntity model)
        {
            return Task.CompletedTask;
        }

        protected internal virtual Task OnAfterSoftDeleteAsync(TEntity model)
        {
            return Task.CompletedTask;
        }
    }
}
