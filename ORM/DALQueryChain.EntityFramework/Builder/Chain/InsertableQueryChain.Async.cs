using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class InsertableQueryChain<TContext, TEntity> : IInsertableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            await _repository.OnBeforeBulkInsertAsync(entities);

            using var trans = await _context.Database.BeginTransactionAsync();

            foreach (var entity in entities)
                _context.Set<TEntity>().Add(entity);

            await _context.SaveChangesAsync();

            await trans.CommitAsync();

            await _repository.OnAfterBulkInsertAsync(entities);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _repository.OnBeforeInsertAsync(entity);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            await _repository.OnAfterInsertAsync(entity);
        }

        public async Task<TEntity> InsertWithObjectAsync(TEntity entity)
        {
            await _repository.OnBeforeInsertAsync(entity);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            await _repository.OnAfterInsertAsync(entity);

            return entity;
        }
    }
}
