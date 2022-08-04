using DALQueryChain.Interfaces;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Repositories
{
    internal class GenericRepository<TContext, TEntity> : BaseRepository<TContext, TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        public GenericRepository(TContext context) : base(context)
        {
        }
    }
}
