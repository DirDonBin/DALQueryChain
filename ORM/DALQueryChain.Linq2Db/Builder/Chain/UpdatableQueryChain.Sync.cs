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
            _repository.OnBeforeBulkUpdate(entities);

            //TODO: Проверить скорость работы
            using var trans = _context.BeginTransaction();

            foreach (var entity in entities)
            {
                _context.Update(entity);
            }

            trans.Commit();

            _repository.OnAfterBulkUpdate(entities);
        }

        public void Update(TEntity entity)
        {
            _repository.OnBeforeUpdate(entity);
            _context.Update(entity);
            _repository.OnAfterUpdate(entity);
        }
    }
}
