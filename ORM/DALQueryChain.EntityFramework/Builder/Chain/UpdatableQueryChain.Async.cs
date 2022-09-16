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
            await _repository.OnBeforeBulkUpdateAsync(entities, ctn);

            //TODO: Проверить скорость работы
            using var trans = await _context.Database.BeginTransactionAsync(ctn);

            foreach (var entity in entities)
            {
                if (ctn.IsCancellationRequested) break;
                _context.Set<TEntity>().Update(entity);
            }

            await _context.SaveChangesAsync(ctn);

            await trans.CommitAsync(ctn);

            await _repository.OnAfterBulkUpdateAsync(entities, ctn);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ctn = default)
        {
            await _repository.OnBeforeUpdateAsync(entity, ctn);
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(ctn);
            await _repository.OnAfterUpdateAsync(entity, ctn);
        }
    }
}
