using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Sync
    public partial interface IDeletableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public void Delete (TEntity entity);
        public void BulkDelete(IEnumerable<TEntity> entities);
        public void BulkDelete(Expression<Func<TEntity, bool>> predicate);

        public void SoftDelete(TEntity entity);
        public void BulkSoftDelete(IEnumerable<TEntity> entities);
        public void BulkSoftDelete(Expression<Func<TEntity, bool>> predicate);
    }
}
