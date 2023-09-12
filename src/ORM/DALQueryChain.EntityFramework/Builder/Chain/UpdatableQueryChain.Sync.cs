using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            using var trans = _context.Database.CurrentTransaction is null
                ? _context.Database.BeginTransaction()
                : null;

            try
            {
                if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                    _repository.InitTriggers(entities);

                if (_repository.IsBeforeTriggerOn && entities.Any())
                    _repository.OnBeforeUpdate();

                foreach (var entity in entities)
                    _context.Set<TEntity>().Update(entity);

                _context.SaveChanges();

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

            using var trans = _context.Database.CurrentTransaction is null
                ? _context.Database.BeginTransaction()
                : null;

            try
            {
                if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                    _repository.InitTriggers(entity);

                if (_repository.IsBeforeTriggerOn)
                    _repository.OnBeforeUpdate();

                _context.Set<TEntity>().Update(entity);
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
