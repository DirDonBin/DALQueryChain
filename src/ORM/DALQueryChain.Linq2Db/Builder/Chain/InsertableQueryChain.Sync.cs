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
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeInsert();

            _context.BulkCopy(entities);

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterInsert();
        }

        public void Insert(TEntity entity)
        {
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeInsert();

            _context.Insert(entity);

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterInsert();
        }

        public TEntity InsertWithObject(TEntity entity)
        {
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeInsert();

            var res = _context.GetTable<TEntity>().InsertWithOutput(entity);

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterInsert();

            return res;
        }
    }
}
