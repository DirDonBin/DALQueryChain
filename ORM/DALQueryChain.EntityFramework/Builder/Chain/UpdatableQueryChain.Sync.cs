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
            _repository.InitTriggers(entities);
            _repository.OnBeforeUpdate();

            //TODO: Проверить скорость работы
            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Update(entity);

            _context.SaveChanges();

            trans.Commit();

            _repository.OnAfterUpdate();
        }

        public void Update(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeUpdate();
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            _repository.OnAfterUpdate();
        }

        public void Update()
        {
            if (_entities is null) throw new InvalidOperationException("Has not been used of method Where");

            _repository.InitTriggers(_entities);
            _repository.OnBeforeUpdate();
            _context.SaveChanges();
            _repository.OnAfterUpdate();
        }
    }
}
