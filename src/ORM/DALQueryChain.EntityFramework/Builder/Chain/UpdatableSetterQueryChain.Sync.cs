using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableSetterQueryChain<TContext, TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public void Update()
        {
            if (_entities is null) throw new InvalidOperationException("Has not been used of method Where");

            using var trans = _context.Database.CurrentTransaction is null
                ? _context.Database.BeginTransaction()
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(_entities);

                if (_repository.IsBeforeTriggerOn)
                    _repository.OnBeforeUpdate();

                _context.SaveChanges();

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
