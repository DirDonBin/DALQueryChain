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
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
        {
            await _repository.OnBeforeBulkInsertAsync(entities, ctn);

            await _context.BulkCopyAsync(entities, ctn);

            await _repository.OnAfterBulkInsertAsync(entities, ctn);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken ctn = default)
        {
            await _repository.OnBeforeInsertAsync(entity, ctn);

            await _context.InsertAsync(entity, token: ctn);

            await _repository.OnAfterInsertAsync(entity, ctn);
        }

        public async Task<TEntity> InsertWithObjectAsync(TEntity entity, CancellationToken ctn = default)
        {
            await _repository.OnBeforeInsertAsync(entity, ctn);

            var res = await _context.GetTable<TEntity>().InsertWithOutputAsync(entity, ctn);

            await _repository.OnAfterInsertAsync(res, ctn);

            return res;
        }
    }
}
