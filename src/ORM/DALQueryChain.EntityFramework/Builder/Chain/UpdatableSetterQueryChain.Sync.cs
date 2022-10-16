using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableSetterQueryChain<TContext, TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
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
