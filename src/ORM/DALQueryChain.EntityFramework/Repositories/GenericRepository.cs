using DALQueryChain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Repositories
{
    internal class GenericRepository<TContext, TEntity> : BaseRepository<TContext, TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        public GenericRepository(TContext context) : base(context)
        {
        }
    }
}
