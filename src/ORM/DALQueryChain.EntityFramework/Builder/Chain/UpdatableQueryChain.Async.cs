using DALQueryChain.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public async Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            using var trans = _context.Database.CurrentTransaction is null
                ? await _context.Database.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                    _repository.InitTriggers(entities);

                if (_repository.IsBeforeTriggerOn && entities.Any())
                    await _repository.OnBeforeUpdate(ctn);

                foreach (var entity in entities)
                {
                    if (ctn.IsCancellationRequested) break;
                    _context.Set<TEntity>().Update(entity);
                }

                await _context.SaveChangesAsync(ctn);

                if (_repository.IsAfterTriggerOn && entities.Any())
                    await _repository.OnAfterUpdate(ctn);

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

        public async Task UpdateAsync(TEntity entity, CancellationToken ctn = default)
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
                    await _repository.OnBeforeUpdate(ctn);

                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync(ctn);

                if (_repository.IsAfterTriggerOn)
                    await _repository.OnAfterUpdate(ctn);

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
    }
}
