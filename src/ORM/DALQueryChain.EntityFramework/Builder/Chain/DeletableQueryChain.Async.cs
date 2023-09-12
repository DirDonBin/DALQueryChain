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
        public async Task BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            //TODO: Проверить скорость работы
            using var trans = _context.Database.CurrentTransaction is null
                ? await _context.Database.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                    _repository.InitTriggers(entities);

                if (_repository.IsBeforeTriggerOn && entities.Any())
                    await _repository.OnBeforeDelete(ctn);

                foreach (var entity in entities)
                {
                    if (ctn.IsCancellationRequested) break;
                    _context.Remove(entity);
                }

                await _context.SaveChangesAsync(ctn);

                if (_repository.IsAfterTriggerOn && entities.Any())
                    await _repository.OnAfterDelete(ctn);

                if (trans is not null)
                    await trans.CommitAsync(ctn);
            }
            catch (Exception)
            {
                if (trans is not null) await trans.RollbackAsync(ctn);

                throw;
            }
        }

        public async Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            var data = await _context.Set<TEntity>().Where(predicate).ToListAsync(ctn);
            await BulkDeleteAsync(data, ctn);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var trans = _context.Database.CurrentTransaction is null
                ? await _context.Database.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(entity);

                if (_repository.IsBeforeTriggerOn)
                    await _repository.OnBeforeDelete(ctn);

                _context.Remove(entity);
                await _context.SaveChangesAsync(ctn);

                if (_repository.IsAfterTriggerOn)
                    await _repository.OnAfterDelete(ctn);

                if (trans is not null)
                    await trans.CommitAsync(ctn);
            }
            catch (Exception)
            {
                if (trans is not null)
                    await trans.RollbackAsync(ctn);

                throw;
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            using var trans = _context.Database.CurrentTransaction is null
                ? await _context.Database.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(predicate);

                if (_repository.IsBeforeTriggerOn)
                    await _repository.OnBeforeDelete(ctn);

                var entities = _context.Set<TEntity>().Where(predicate);

                foreach (var entity in entities)
                {
                    if (ctn.IsCancellationRequested) break;
                    _context.Remove(entity);
                }

                await _context.SaveChangesAsync(ctn);

                if (_repository.IsAfterTriggerOn)
                    await _repository.OnAfterDelete(ctn);

                if (trans is not null)
                    await trans.CommitAsync(ctn);
            }
            catch (Exception)
            {
                if (trans is not null)
                    await trans.RollbackAsync(ctn);

                throw;
            }
        }

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entity">Entity model for delete</param>
        public Task SoftDeleteAsync(TEntity entity, CancellationToken ctn = default) => _repository.SoftDelete(entity, ctn);

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public Task SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => _repository.SoftDelete(predicate, ctn);

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entities">Entity models for delete</param>
        public Task BulkSoftDeleteAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default) => _repository.SoftBulkDelete(entities, ctn);

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public Task BulkSoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default) => _repository.SoftBulkDelete(predicate, ctn);
    }
}
