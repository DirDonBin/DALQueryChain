using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
    }
}
