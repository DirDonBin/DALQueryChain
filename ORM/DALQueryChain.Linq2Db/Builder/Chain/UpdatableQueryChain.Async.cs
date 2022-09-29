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
            _repository.InitTriggers(entities);
            await _repository.OnBeforeUpdate(ctn);
            await _context.UpdateAsync(entities, token: ctn);
            await _repository.OnAfterUpdate(ctn);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ctn = default)
        {
            _repository.InitTriggers(entity);
            await _repository.OnBeforeUpdate(ctn);
            await _context.UpdateAsync(entity, token: ctn);
            await _repository.OnAfterUpdate(ctn);
        }
    }
}
