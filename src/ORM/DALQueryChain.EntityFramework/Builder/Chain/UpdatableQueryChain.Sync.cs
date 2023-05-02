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

            if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn && entities.Any())
                _repository.OnBeforeUpdate();

            //TODO: Проверить скорость работы
            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Update(entity);

            _context.SaveChanges();

            trans.Commit();

            if (_repository.IsAfterTriggerOn && entities.Any())
                _repository.OnAfterUpdate();
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeUpdate();

            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterUpdate();
        }
    }
}
