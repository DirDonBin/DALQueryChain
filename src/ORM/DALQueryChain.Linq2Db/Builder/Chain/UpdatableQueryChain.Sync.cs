using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            _repository.InitTriggers(entities);
            _repository.OnBeforeUpdate();

            using var transaction = _context.BeginTransaction();

            foreach (var entity in entities)
                _context.Update(entity);

            transaction.Commit();

            _repository.OnAfterUpdate();
        }

        public void Update(TEntity entity)
        {
            _repository.InitTriggers(entity);
            _repository.OnBeforeUpdate();
            _context.Update(entity);
            _repository.OnAfterUpdate();
        }
    }
}
