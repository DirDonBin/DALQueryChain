using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeUpdate();

            using var transaction = _context.BeginTransaction();

            foreach (var entity in entities)
                _context.Update(entity);

            transaction.Commit();

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterUpdate();
        }

        public void Update(TEntity entity)
        {
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeUpdate();

            _context.Update(entity);

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterUpdate();
        }
    }
}
