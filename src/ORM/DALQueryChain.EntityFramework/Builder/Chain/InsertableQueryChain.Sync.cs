using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class InsertableQueryChain<TContext, TEntity> : IInsertableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            if ((_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn) && entities.Any())
                _repository.InitTriggers(entities);

            if (_repository.IsBeforeTriggerOn && entities.Any())
                _repository.OnBeforeInsert();

            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

            trans.Commit();

            if (_repository.IsAfterTriggerOn && entities.Any())
                _repository.OnAfterInsert();
        }

        public void Insert(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeInsert();

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterInsert();
        }

        public TEntity InsertWithObject(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (_repository.IsBeforeTriggerOn || _repository.IsAfterTriggerOn)
                _repository.InitTriggers(entity);

            if (_repository.IsBeforeTriggerOn)
                _repository.OnBeforeInsert();

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            if (_repository.IsAfterTriggerOn)
                _repository.OnAfterInsert();

            return entity;
        }
    }
}
