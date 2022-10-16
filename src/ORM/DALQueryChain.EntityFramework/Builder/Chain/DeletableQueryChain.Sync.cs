using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public void BulkDelete(IEnumerable<TEntity> entities)
        {
            _repository.InitTriggers(entities);
            _repository.OnBeforeDelete();

            //TODO: Проверить скорость работы
            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Remove(entity);

            _context.SaveChanges();

            trans.Commit();

            _repository.OnAfterDelete();
        }

        public void BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var data = _context.Set<TEntity>().Where(predicate).ToList();
            BulkDelete(data);
        }

        public void Delete(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeDelete();
            _context.Remove(entity);
            _context.SaveChanges();
            _repository.OnAfterDelete();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _repository.InitTriggers(predicate);
            _repository.OnBeforeDelete();

            var entities = _context.Set<TEntity>().Where(predicate);

            //TODO: Проверить скорость работы
            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Remove(entity);

            _context.SaveChanges();

            trans.Commit();

            _context.SaveChanges();
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
        /// <param name="entity">Entity model for delete</param>
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
