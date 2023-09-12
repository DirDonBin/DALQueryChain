using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableSetterQueryChain<TContext, TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public void Update()
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            if (_prevUpdateQuery is null) throw new InvalidOperationException("No update entity values ​​set. Use Method 'Set'"); ;

            using var trans = _context.Transaction is null
                ? _context.BeginTransaction()
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(_prevQuery);

                if (_repository.IsBeforeTriggerOn)
                    _repository.OnBeforeUpdate();

                _prevUpdateQuery.Update();

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
