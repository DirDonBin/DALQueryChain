using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Sync
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public void Update(TEntity entity);
        public void BulkUpdate(IEnumerable<TEntity> entities);
    }
}
