using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
