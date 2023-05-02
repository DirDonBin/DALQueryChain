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
            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeUpdate();

            //TODO: Проверить скорость работы
            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Update(entity);

            _context.SaveChanges();

            trans.Commit();

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterUpdate();
        }

        public void Update(TEntity entity)
        {
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
