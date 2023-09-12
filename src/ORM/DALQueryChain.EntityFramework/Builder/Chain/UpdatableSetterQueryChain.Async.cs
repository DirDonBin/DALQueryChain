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

            using var trans = _context.Database.CurrentTransaction is null
                ? await _context.Database.BeginTransactionAsync(ctn)
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(_entities);

                if (_repository.IsBeforeTriggerOn)
                    await _repository.OnBeforeUpdate(ctn); 

                await _context.SaveChangesAsync(ctn);

                if (_repository.IsAfterTriggerOn)
                    await _repository.OnAfterUpdate(ctn);

                if (trans is not null)
                    await trans.CommitAsync(ctn);
            }
            catch (Exception)
            {
                if (trans is not null)
                    await trans.RollbackAsync(ctn);

                throw;
            }

        }
    }
}
