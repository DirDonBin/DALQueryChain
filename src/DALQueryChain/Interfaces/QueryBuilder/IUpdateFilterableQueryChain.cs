using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public interface IUpdateFilterableQueryChain<TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TEntity : class, IDbModelBase
    {
        public IUpdateFilterableQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        public IUpdateFilterableQueryChain<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> predicate);

        public IUpdateFilterableQueryChain<TEntity> When(bool condition, Func<IUpdateFilterableQueryChain<TEntity>, IUpdateFilterableQueryChain<TEntity>> query);
    }
}
