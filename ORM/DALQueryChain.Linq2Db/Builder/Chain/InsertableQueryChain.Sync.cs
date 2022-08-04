using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class InsertableQueryChain<TContext, TEntity> : IInsertableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            _repository.OnBeforeBulkInsert(entities);

            _context.BulkCopy(entities);

            _repository.OnAfterBulkInsert(entities);
        }

        public void Insert(TEntity entity)
        {
            _repository.OnBeforeInsert(entity);

            _context.Insert(entity);

            _repository.OnAfterInsert(entity);
        }

        public TEntity InsertWithObject(TEntity entity)
        {
            _repository.OnBeforeInsert(entity);

            var res = _context.GetTable<TEntity>().InsertWithOutput(entity);

            _repository.OnAfterInsert(res);

            return res;
        }
    }
}
