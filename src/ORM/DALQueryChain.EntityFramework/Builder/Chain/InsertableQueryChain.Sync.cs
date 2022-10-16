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
            _repository.InitTriggers(entities);
            _repository.OnBeforeInsert();

            using var trans = _context.Database.BeginTransaction();

            foreach (var entity in entities)
                _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

            trans.Commit();

            _repository.OnAfterInsert();
        }

        public void Insert(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeInsert();

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            _repository.OnAfterInsert();
        }

        public TEntity InsertWithObject(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeInsert();

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            _repository.OnAfterInsert();

            return entity;
        }
    }
}
