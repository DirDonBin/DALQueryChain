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
            ArgumentNullException.ThrowIfNull(entities);

            using var trans = _context.Database.CurrentTransaction is null
                ? _context.Database.BeginTransaction()
                : null;

            try
            {
                if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                    _repository.InitTriggers(entities);

                if (_repository.IsBeforeTriggerOn && entities.Any())
                    _repository.OnBeforeDelete();

                foreach (var entity in entities)
                    _context.Remove(entity);

                _context.SaveChanges();

                if (_repository.IsAfterTriggerOn && entities.Any())
                    _repository.OnAfterDelete();

                trans?.Commit();
            }
            catch (Exception)
            {
                trans?.Rollback();

                throw;
            }
        }

        public void BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var data = _context.Set<TEntity>().Where(predicate).ToList();
            BulkDelete(data);
        }

        public void Delete(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var trans = _context.Database.CurrentTransaction is null
                ? _context.Database.BeginTransaction()
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(entity);

                if (_repository.IsBeforeTriggerOn)
                    _repository.OnBeforeDelete();

                _context.Remove(entity);
                _context.SaveChanges();

                if (_repository.IsAfterTriggerOn)
                    _repository.OnAfterDelete();

                trans?.Commit();
            }
            catch (Exception)
            {
                trans?.Rollback();

                throw;
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            using var trans = _context.Database.CurrentTransaction is null
                ? _context.Database.BeginTransaction()
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(predicate);

                if (_repository.IsBeforeTriggerOn)
                    _repository.OnBeforeDelete();

                var entities = _context.Set<TEntity>().Where(predicate);

                foreach (var entity in entities)
                    _context.Remove(entity);

                _context.SaveChanges();

                if (_repository.IsAfterTriggerOn)
                    _repository.OnAfterDelete();

                trans?.Commit();
            }
            catch (Exception)
            {
                trans?.Rollback();

                throw;
            }
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
