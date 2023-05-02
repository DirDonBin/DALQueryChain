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

            if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn && entities.Any())
                await _repository.OnBeforeInsert(ctn);

            await _context.BulkCopyAsync(entities, ctn);

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

            await _context.InsertAsync(entity, token: ctn);

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

            var res = await _context.GetTable<TEntity>().InsertWithOutputAsync(entity, ctn);

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(res);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterInsert(ctn);

            return res;
        }
    }
}
