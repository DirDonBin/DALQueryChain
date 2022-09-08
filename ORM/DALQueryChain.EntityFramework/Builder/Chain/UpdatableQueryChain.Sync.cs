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
            _repository.OnBeforeBulkUpdate(entities);

            //TODO: Проверить скорость работы
            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Update(entity);

            _context.SaveChanges();

            trans.Commit();

            _repository.OnAfterBulkUpdate(entities);
        }

        public void Update(TEntity entity)
        {
            _repository.OnBeforeUpdate(entity);
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            _repository.OnAfterUpdate(entity);
        }
    }
}
