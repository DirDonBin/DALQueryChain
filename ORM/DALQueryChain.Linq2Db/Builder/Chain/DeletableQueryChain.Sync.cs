using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public void BulkDelete(IEnumerable<TEntity> entities)
        {
            _repository.InitTriggers(entities);

            _repository.OnBeforeDelete();
            _context.Delete(entities);
            _repository.OnAfterDelete();
        }

        public void BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            _repository.InitTriggers(predicate);

            _repository.OnBeforeDelete();
            _context.GetTable<TEntity>().Where(predicate).Delete();
            _repository.OnAfterDelete();
        }

        public void Delete(TEntity entity)
        {
            _repository.InitTriggers(entity);

            _repository.OnBeforeDelete();
            _context.Delete(entity);
            _repository.OnAfterDelete();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _repository.InitTriggers(predicate);

            _repository.OnBeforeDelete();
            _context.GetTable<TEntity>().Where(predicate).Delete();
            _repository.OnAfterDelete();
        }

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entity">Entity model for delete</param>
        public void SoftDelete(TEntity entity) => _repository.SoftDelete(entity);

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public void SoftDelete(Expression<Func<TEntity, bool>> predicate) => _repository.SoftDelete(predicate);

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entities">Entity models for delete</param>
        public void BulkSoftDelete(IEnumerable<TEntity> entities) => _repository.SoftBulkDelete(entities);

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public void BulkSoftDelete(Expression<Func<TEntity, bool>> predicate) => _repository.SoftBulkDelete(predicate);
    }
}
