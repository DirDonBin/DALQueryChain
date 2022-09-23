using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using System.Linq.Expressions;

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

        public async Task UpdateAsync(CancellationToken ctn = default)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            if (_prevUpdateQuery is null) throw new InvalidOperationException("No update entity values ​​set. Use Method 'Set'"); ;

            _repository.InitTriggers(_prevQuery);
            await _repository.OnBeforeUpdate(ctn);
            await _prevUpdateQuery.UpdateAsync(ctn);
            await _repository.OnAfterUpdate(ctn);
        }
    }
}
