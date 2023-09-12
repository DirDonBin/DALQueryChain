using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class InsertableQueryChain<TContext, TEntity> : IInsertableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            using var trans = _context.Transaction is null
                ? await _context.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                    _repository.InitTriggers(entities);

                if (_repository.IsBeforeTriggerOn && entities.Any())
                    await _repository.OnBeforeInsert(ctn);

                await _context.BulkCopyAsync(entities, ctn);

                if (_repository.IsAfterTriggerOn && entities.Any())
                    await _repository.OnAfterInsert(ctn);

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

        public async Task InsertAsync(TEntity entity, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var trans = _context.Transaction is null
                ? await _context.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(entity);

                if (_repository.IsBeforeTriggerOn)
                    await _repository.OnBeforeInsert(ctn);

                await _context.InsertAsync(entity, token: ctn);

                if (_repository.IsAfterTriggerOn)
                    await _repository.OnAfterInsert(ctn);

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

        public async Task<TEntity> InsertWithObjectAsync(TEntity entity, CancellationToken ctn = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var trans = _context.Transaction is null
               ? await _context.BeginTransactionAsync(ctn)
               : null;

            TEntity res = default!;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(entity);

                if (_repository.IsBeforeTriggerOn)
                    await _repository.OnBeforeInsert(ctn);

                res = await _context.GetTable<TEntity>().InsertWithOutputAsync(entity, ctn);

                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(res);

                if (_repository.IsAfterTriggerOn)
                    await _repository.OnAfterInsert(ctn);

                if (trans is not null)
                    await trans.CommitAsync(ctn);
            }
            catch (Exception)
            {
                if (trans is not null)
                    await trans.RollbackAsync(ctn);

                throw;
            }

            return res;
        }
    }
}
