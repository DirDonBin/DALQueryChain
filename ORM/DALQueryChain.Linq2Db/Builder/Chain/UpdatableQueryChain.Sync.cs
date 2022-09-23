using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

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

        public void Update()
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            if (_prevUpdateQuery is null) throw new InvalidOperationException("No update entity values ​​set. Use Method 'Set'"); ;

            _repository.InitTriggers(_prevQuery);
            _repository.OnBeforeUpdate();
            _prevUpdateQuery.Update();
            _repository.OnAfterUpdate();
        }
    }
}
