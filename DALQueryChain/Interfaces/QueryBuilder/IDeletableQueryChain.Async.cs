using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IDeletableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task DeleteAsync(TEntity entity);
        public Task BulkDeleteAsync(IEnumerable<TEntity> entities);
        public Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate);

        public Task SoftDeleteAsync(TEntity entity);
        public Task BulkSoftDeleteAsync(IEnumerable<TEntity> entities);
        public Task BulkSoftDeleteAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
