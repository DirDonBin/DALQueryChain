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

        public async Task UpdateAsync(CancellationToken ctn = default)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            if (_prevUpdateQuery is null) throw new InvalidOperationException("No update entity values ​​set. Use Method 'Set'"); ;

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(_prevQuery);

            if (_repository.IsBeforeTriggerOn)
                await _repository.OnBeforeUpdate(ctn);

            await _prevUpdateQuery.UpdateAsync(ctn);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterUpdate(ctn);
        }
    }
}
