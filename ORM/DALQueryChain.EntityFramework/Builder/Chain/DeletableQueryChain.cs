using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _repository;
        private readonly TContext _context;

        public DeletableQueryChain(TContext context, BaseRepository<TContext, TEntity> repository)
        {
            _repository = repository;
            _context = context;
        }
    }
}
