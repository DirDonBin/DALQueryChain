using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableSetterQueryChain<TContext, TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {

        public async Task UpdateAsync(CancellationToken ctn = default)
        {
            if (_entities is null) throw new InvalidOperationException("Has not been used of method Where");

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(_entities);

            if (_repository.IsBeforeTriggerOn)
                await _repository.OnBeforeUpdate(ctn);

            await _context.SaveChangesAsync(ctn);

            if (_repository.IsAfterTriggerOn)
                await _repository.OnAfterUpdate(ctn);
        }
    }
}
