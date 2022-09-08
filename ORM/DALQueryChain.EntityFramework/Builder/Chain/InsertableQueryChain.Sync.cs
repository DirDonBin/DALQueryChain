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
            _repository.OnBeforeBulkInsert(entities);

            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

            trans.Commit();

            _repository.OnAfterBulkInsert(entities);
        }

        public void Insert(TEntity entity)
        {
            _repository.OnBeforeInsert(entity);

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            _repository.OnAfterInsert(entity);
        }

        public TEntity InsertWithObject(TEntity entity)
        {
            _repository.OnBeforeInsert(entity);

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            _repository.OnAfterInsert(entity);

            return entity;
        }
    }
}
