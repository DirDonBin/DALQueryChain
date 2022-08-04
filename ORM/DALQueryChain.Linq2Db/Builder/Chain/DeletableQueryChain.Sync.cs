using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Expressions;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public void BulkDelete(IEnumerable<TEntity> entities)
        {
            _repository.OnBeforeBulkDelete(entities);

            //TODO: Проверить скорость работы
            using var trans = _context.BeginTransaction();

            foreach (var entity in entities)
            {
                _context.Delete(entity);
            }

            trans.Commit();

            _repository.OnAfterBulkDelete(entities);
        }

        public void BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var data = _context.GetTable<TEntity>().Where(predicate).ToList();
            BulkDelete(data);
        }

        public void Delete(TEntity entity)
        {
            _repository.OnBeforeDelete(entity);
            _context.Delete(entity);
            _repository.OnAfterDelete(entity);
        }

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entity">Entity model for delete</param>
        public void SoftDelete(TEntity entity)
        {
            _repository.SoftDelete(entity);
        }

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entities">Entity models for delete</param>
        public void BulkSoftDelete(IEnumerable<TEntity> entities)
        {
            _repository.SoftBulkDelete(entities);
        }

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public void BulkSoftDelete(Expression<Func<TEntity, bool>> predicate)
        {
            _repository.SoftBulkDelete(predicate);
        }
    }
}
