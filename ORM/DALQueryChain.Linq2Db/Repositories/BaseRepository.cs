using DALQueryChain.Interfaces;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Linq2Db.Repositories
{
    public abstract partial class BaseRepository<TContext, TEntity> : IRepository
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        protected IQueryable<TEntity> _query;

        public BaseRepository(TContext context)
        {
            _query = context.GetTable<TEntity>().AsQueryable();
        }

        
    }
}
