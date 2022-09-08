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
        #endregion

        #region Single Operations
        protected internal virtual void OnBeforeInsert(TEntity model)
        {

        }

        protected internal virtual void OnAfterInsert(TEntity model)
        {

        }

        protected internal virtual void OnBeforeUpdate(TEntity model)
        {

        }

        protected internal virtual void OnAfterUpdate(TEntity model)
        {

        }

        protected internal virtual void OnBeforeDelete(TEntity model)
        {

        }

        protected internal virtual void OnAfterDelete(TEntity model)
        {

        }
        #endregion


        #endregion

        protected internal virtual void SoftDelete(TEntity model)
        {
            
        }

        protected internal virtual void SoftBulkDelete(IEnumerable<TEntity> model)
        {

        }

        protected internal virtual void SoftBulkDelete(Expression<Func<TEntity, bool>> predicate)
        {

        }
    }
}
