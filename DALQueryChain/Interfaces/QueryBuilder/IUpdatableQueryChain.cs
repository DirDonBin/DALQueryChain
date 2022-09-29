using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public IUpdatableSetterQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
