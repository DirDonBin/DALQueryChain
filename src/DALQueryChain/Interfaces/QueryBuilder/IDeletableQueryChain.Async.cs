using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IDeletableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task DeleteAsync(TEntity entity, CancellationToken ctn = default);
        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default);
        public Task BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default);
        public Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default);

        public Task SoftDeleteAsync(TEntity entity, CancellationToken ctn = default);
        public Task SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default);
        public Task BulkSoftDeleteAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default);
        public Task BulkSoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ctn = default);
    }
}
