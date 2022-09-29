using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class UpdatableSetterQueryChain<TContext, TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _repository;
        private readonly TContext _context;
        private readonly IEnumerable<TEntity>? _entities = null;

        public UpdatableSetterQueryChain(TContext context, BaseRepository<TContext, TEntity> repository)
        {
            _repository = repository;
            _context = context;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, TV value)
        {
            if (_entities is null) throw new InvalidOperationException("Has not been used of method Where");

            _context.Set<TEntity>().AttachRange(_entities);

            foreach (var entity in _entities)
                _context.Entry(entity).Property(extract).CurrentValue = value;

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
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
    }
}
