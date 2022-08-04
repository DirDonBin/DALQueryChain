using DALQueryChain.Interfaces.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces
{
    public interface IDALQueryChain<TContext>
        where TContext : class
    {
        public IQueryBuilder<TEntity> For<TEntity>() where TEntity : class, IDbModelBase;
    }
}
