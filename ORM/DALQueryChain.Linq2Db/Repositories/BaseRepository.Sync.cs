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
        protected internal virtual void OnBeforeBulkInsert(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnBeforeInsert(item);
        }

        protected internal virtual void OnAfterBulkInsert(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnAfterInsert(item);
        }

        protected internal virtual void OnBeforeInsert(TEntity model)
        {
            
        }

        protected internal virtual void OnAfterInsert(TEntity model)
        {

        }

        protected internal virtual void OnBeforeBulkUpdate(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnBeforeUpdate(item);
        }

        protected internal virtual void OnAfterBulkUpdate(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnAfterUpdate(item);
        }

        protected internal virtual void OnBeforeUpdate(TEntity model)
        {

        }

        protected internal virtual void OnAfterUpdate(TEntity model)
        {

        }

        protected internal virtual void OnBeforeBulkDelete(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnBeforeDelete(item);
        }

        protected internal virtual void OnAfterBulkDelete(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnAfterDelete(item);
        }

        protected internal virtual void OnBeforeDelete(TEntity model)
        {

        }

        protected internal virtual void OnAfterDelete(TEntity model)
        {

        }

        protected internal virtual void OnBeforeBulkSoftDelete(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnBeforeSoftDelete(item);
        }

        protected internal virtual void OnAfterBulkSoftDelete(IEnumerable<TEntity> models)
        {
            foreach (var item in models)
                OnAfterSoftDelete(item);
        }

        protected internal virtual void OnBeforeSoftDelete(TEntity model)
        {

        }

        protected internal virtual void OnAfterSoftDelete(TEntity model)
        {

        }
    }
}
