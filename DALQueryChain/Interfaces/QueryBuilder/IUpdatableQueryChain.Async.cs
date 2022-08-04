using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task UpdateAsync(TEntity entity);
        public Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    }
}
