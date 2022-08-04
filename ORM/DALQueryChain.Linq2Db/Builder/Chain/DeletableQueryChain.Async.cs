using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public async Task BulkDeleteAsync(IEnumerable<TEntity> entities)
        {
            await _repository.OnBeforeBulkDeleteAsync(entities);

            //TODO: Проверить скорость работы
            using var trans = await _context.BeginTransactionAsync();

            foreach (var entity in entities)
            {
                await _context.UpdateAsync(entity);
            }

            await trans.CommitAsync();

            await _repository.OnAfterBulkDeleteAsync(entities);
        }

        public async Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await _context.GetTable<TEntity>().Where(predicate).ToListAsync();
            await BulkDeleteAsync(data);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.OnBeforeDeleteAsync(entity);
            await _context.DeleteAsync(entity);
            await _repository.OnAfterDeleteAsync(entity);
        }

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entity">Entity model for delete</param>
        public async Task SoftDeleteAsync(TEntity entity)
        {
            await _repository.SoftDeleteAsync(entity);
        }

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entities">Entity models for delete</param>
        public async Task BulkSoftDeleteAsync(IEnumerable<TEntity> entities)
        {
            await _repository.SoftBulkDeleteAsync(entities);
        }

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public async Task BulkSoftDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await _repository.SoftBulkDeleteAsync(predicate);
        }
    }
}
