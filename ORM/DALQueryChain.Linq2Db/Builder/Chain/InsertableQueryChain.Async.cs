using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class InsertableQueryChain<TContext, TEntity> : IInsertableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            await _repository.OnBeforeBulkInsertAsync(entities);

            await _context.BulkCopyAsync(entities);

            await _repository.OnAfterBulkInsertAsync(entities);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _repository.OnBeforeInsertAsync(entity);

            await _context.InsertAsync(entity);

            await _repository.OnAfterInsertAsync(entity);
        }

        public async Task<TEntity> InsertWithObjectAsync(TEntity entity)
        {
            await _repository.OnBeforeInsertAsync(entity);

            var res = await _context.GetTable<TEntity>().InsertWithOutputAsync(entity);

            await _repository.OnAfterInsertAsync(res);

            return res;
        }
    }
}
