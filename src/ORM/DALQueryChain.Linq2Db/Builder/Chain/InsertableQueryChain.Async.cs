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
            _repository.InitTriggers(entities);
            await _repository.OnBeforeInsert(ctn);
            await _context.BulkCopyAsync(entities, ctn);
            await _repository.OnAfterInsert(ctn);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken ctn = default)
        {
            _repository.InitTriggers(entity);
            await _repository.OnBeforeInsert(ctn);
            await _context.InsertAsync(entity, token: ctn);
            await _repository.OnAfterInsert(ctn);
        }

        public async Task<TEntity> InsertWithObjectAsync(TEntity entity, CancellationToken ctn = default)
        {
            _repository.InitTriggers(entity);
            await _repository.OnBeforeInsert(ctn);
            var res = await _context.GetTable<TEntity>().InsertWithOutputAsync(entity, ctn);
            await _repository.OnAfterInsert(ctn);
            return res;
        }
    }
}
