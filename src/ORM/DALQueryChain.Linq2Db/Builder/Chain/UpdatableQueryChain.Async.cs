using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public async Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn)
                await _repository.OnBeforeUpdate(ctn);

            using var transaction = await _context.BeginTransactionAsync();

            foreach (var entity in entities)
            {
                if (ctn.IsCancellationRequested) break;
                await _context.UpdateAsync(entity, token: ctn);
            }

            await transaction.CommitAsync(ctn);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterUpdate(ctn);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ctn = default)
        {
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                await _repository.OnBeforeUpdate(ctn);

            await _context.UpdateAsync(entity, token: ctn);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterUpdate(ctn);
        }
    }
}
