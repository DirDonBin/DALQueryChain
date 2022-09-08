using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
        {
            await _repository.OnBeforeBulkUpdateAsync(entities);

            //TODO: Проверить скорость работы
            using var trans = await _context.Database.BeginTransactionAsync();

            foreach (var entity in entities)
                _context.Set<TEntity>().Update(entity);

            await _context.SaveChangesAsync();

            await trans.CommitAsync();

            await _repository.OnAfterBulkUpdateAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.OnBeforeUpdateAsync(entity);
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            await _repository.OnAfterUpdateAsync(entity);
        }
    }
}
