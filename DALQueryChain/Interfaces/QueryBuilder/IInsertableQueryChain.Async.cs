using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IInsertableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task InsertAsync(TEntity entity);
        public Task<TEntity> InsertWithObjectAsync(TEntity entity);
        public Task BulkInsertAsync(IEnumerable<TEntity> entity);
    }
}
