using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Sync
    public partial interface IDeletableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public void Delete(TEntity entity);
        public void BulkDelete(IEnumerable<TEntity> entities);
        public void BulkDelete(Expression<Func<TEntity, bool>> predicate);

        public void SoftDelete(TEntity entity);
        public void BulkSoftDelete(IEnumerable<TEntity> entities);
        public void BulkSoftDelete(Expression<Func<TEntity, bool>> predicate);
    }
}
