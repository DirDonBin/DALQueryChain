using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        private BaseRepository<TContext, TEntity> _repository;
        private TContext _context;
        private IEnumerable<TEntity>? _entities = null;

        public UpdatableQueryChain(TContext context, BaseRepository<TContext, TEntity> repository)
        {
            _repository = repository;
            _context = context;
        }

        public IUpdatableQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, TV value)
        {
            if (_entities is null) throw new InvalidOperationException("Has not been used of method Where");

            _context.Set<TEntity>().AttachRange(_entities);

            foreach (var entity in _entities)
                _context.Entry(entity).Property(extract).CurrentValue = value;

            return this;
        }

        public IUpdatableQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
        {
            if (_entities is null) throw new InvalidOperationException("Has not been used of method Where");

            _context.Set<TEntity>().AttachRange(_entities);

            foreach (var entity in _entities)
            {
                var comp = Expression.Lambda<Func<TV>>(Expression.Invoke(value)).Compile();
                _context.Entry(entity).Property(extract).CurrentValue = comp();
            }

            return this;
        }

        public IUpdatableQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _entities = _context.Set<TEntity>().Where(predicate).ToList();
            return this;
        }
    }
}
