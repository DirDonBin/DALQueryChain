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
            await _repository.OnBeforeBulkUpdateAsync(entities, ctn);

            //TODO: Проверить скорость работы
            using var trans = await _context.BeginTransactionAsync(ctn);

            foreach (var entity in entities)
            {
                if (ctn.IsCancellationRequested)
                    break;

                await _context.UpdateAsync(entity, token: ctn);
            }

            await trans.CommitAsync();

            await _repository.OnAfterBulkUpdateAsync(entities, ctn);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ctn = default)
        {
            await _repository.OnBeforeUpdateAsync(entity, ctn);
            await _context.UpdateAsync(entity, token: ctn);
            await _repository.OnAfterUpdateAsync(entity, ctn);
        }
    }
}
