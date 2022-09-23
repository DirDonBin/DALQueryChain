using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public IUpdatableQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        public IUpdatableQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, TV value);
        public IUpdatableQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value);
    }
}
