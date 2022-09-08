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
        public async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
        {
            await _repository.OnBeforeBulkUpdateAsync(entities);

            //TODO: Проверить скорость работы
            using var trans = await _context.BeginTransactionAsync();

            foreach (var entity in entities)
                await _context.UpdateAsync(entity);

            await trans.CommitAsync();

            await _repository.OnAfterBulkUpdateAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.OnBeforeUpdateAsync(entity);
            await _context.UpdateAsync(entity);
            await _repository.OnAfterUpdateAsync(entity);
        }
    }
}
