using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public async Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            _repository.InitTriggers(entities);
            await _repository.OnBeforeUpdate(ctn);

            //TODO: Проверить скорость работы
            using var trans = await _context.Database.BeginTransactionAsync(ctn);

            foreach (var entity in entities)
            {
                if (ctn.IsCancellationRequested) break;
                _context.Set<TEntity>().Update(entity);
            }

            await _context.SaveChangesAsync(ctn);

            await trans.CommitAsync(ctn);

            await _repository.OnAfterUpdate(ctn);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ctn = default)
        {
            _repository.InitTriggers(entity);
            await _repository.OnBeforeUpdate(ctn);
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(ctn);
            await _repository.OnAfterUpdate(ctn);
        }
    }
}
