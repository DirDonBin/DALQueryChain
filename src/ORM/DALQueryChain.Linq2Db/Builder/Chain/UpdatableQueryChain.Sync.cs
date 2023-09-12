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
            ArgumentNullException.ThrowIfNull(entities);

            using var trans = _context.Transaction is null
                ? _context.BeginTransaction()
                : null;

            try
            {
                if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                    _repository.InitTriggers(entities);

                if (_repository.IsBeforeTriggerOn && entities.Any())
                    _repository.OnBeforeUpdate();

                foreach (var entity in entities)
                    _context.Update(entity);

                if (_repository.IsAfterTriggerOn && entities.Any())
                    _repository.OnAfterUpdate();

                trans?.Commit();
            }
            catch (Exception)
            {
                trans?.Rollback();

                throw;
            }
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var trans = _context.Transaction is null
                ? _context.BeginTransaction()
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(entity);

                if (_repository.IsBeforeTriggerOn)
                    _repository.OnBeforeUpdate();

                _context.Update(entity);

                if (_repository.IsAfterTriggerOn)
                    _repository.OnAfterUpdate();

                trans?.Commit();
            }
            catch (Exception)
            {
                trans?.Rollback();

                throw;
            }
        }
    }
}
