﻿using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public async Task BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            await _repository.OnBeforeBulkDeleteAsync(entities, ctn);

            //TODO: Проверить скорость работы
            using var trans = await _context.Database.BeginTransactionAsync(ctn);

            foreach (var entity in entities)
            {
                if (ctn.IsCancellationRequested) break;
                _context.Remove(entity);
            }

            await _context.SaveChangesAsync(ctn);

            await trans.CommitAsync(ctn);

            await _repository.OnAfterBulkDeleteAsync(entities, ctn);
        }

        public async Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default)
        {
            var data = await _context.Set<TEntity>().Where(predicate).ToListAsync(ctn);
            await BulkDeleteAsync(data, ctn);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ctn = default)
        {
            await _repository.OnBeforeDeleteAsync(entity, ctn);
            _context.Remove(entity);
            await _context.SaveChangesAsync(ctn);
            await _repository.OnAfterDeleteAsync(entity, ctn);
        }

        /// <summary>
        /// Soft Delete record. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entity">Entity model for delete</param>
        public async Task SoftDeleteAsync(TEntity entity, CancellationToken ctn = default)
        {
            await _repository.SoftDeleteAsync(entity, ctn);
        }

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="entities">Entity models for delete</param>
        public async Task BulkSoftDeleteAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            await _repository.SoftBulkDeleteAsync(entities, ctn);
        }

        /// <summary>
        /// Soft Delete records. Need to override the SoftDelete method in the repository
        /// </summary>
        /// <param name="predicate">Сondition for entries to be deleted</param>
        public async Task BulkSoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default)
        {
            await _repository.SoftBulkDeleteAsync(predicate, ctn);
        }
    }
}
