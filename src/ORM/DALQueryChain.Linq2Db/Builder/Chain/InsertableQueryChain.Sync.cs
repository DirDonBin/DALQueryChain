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
        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            _repository.InitTriggers(entities);
            _repository.OnBeforeInsert();
            _context.BulkCopy(entities);
            _repository.OnAfterInsert();
        }

        public void Insert(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeInsert();
            _context.Insert(entity);
            _repository.OnAfterInsert();
        }

        public TEntity InsertWithObject(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeInsert();
            var res = _context.GetTable<TEntity>().InsertWithOutput(entity);
            _repository.OnAfterInsert();
            return res;
        }
    }
}
