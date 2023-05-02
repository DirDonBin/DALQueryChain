using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class InsertableQueryChain<TContext, TEntity> : IInsertableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn && entities.Any())
                await _repository.OnBeforeInsert(ctn);

            using var trans = await _context.Database.BeginTransactionAsync(ctn);

            foreach (var entity in entities)
            {
                if (ctn.IsCancellationRequested) break;
                _context.Set<TEntity>().Add(entity);
            }

            await _context.SaveChangesAsync(ctn);

            await trans.CommitAsync(ctn);

            if (_repository.IsAfterTriggerOn && entities.Any())
                await _repository.OnAfterInsert(ctn);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                await _repository.OnBeforeInsert(ctn);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync(ctn);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterInsert(ctn);
        }

        public async Task<TEntity> InsertWithObjectAsync(TEntity entity, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                await _repository.OnBeforeInsert(ctn);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync(ctn);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterInsert(ctn);

            return entity;
        }
    }
}
