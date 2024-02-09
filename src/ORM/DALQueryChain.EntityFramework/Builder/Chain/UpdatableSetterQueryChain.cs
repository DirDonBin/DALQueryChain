using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Enums;
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
        private IEnumerable<TEntity>? _entities;
        protected IQueryable<TEntity> _prevQuery;

        public UpdatableSetterQueryChain(TContext context, BaseRepository<TContext, TEntity> repository, IQueryable<TEntity> prevQuery)
        {
            _repository = repository;
            _context = context;
            _prevQuery = prevQuery;

            _repository.IsBeforeTriggerOn = true;
            _repository.IsAfterTriggerOn = true;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, TV value)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            _entities ??= _prevQuery.ToList();

            _context.Set<TEntity>().AttachRange(_entities);

            foreach (var entity in _entities)
                _context.Entry(entity).Property(extract).CurrentValue = value;

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            _entities ??= _prevQuery.ToList();

            _context.Set<TEntity>().AttachRange(_entities);

            foreach (var entity in _entities)
            {
                var comp = Expression.Lambda<Func<TV>>(Expression.Invoke(value)).Compile();
                _context.Entry(entity).Property(extract).CurrentValue = comp();
            }

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TEntity, TV>> update)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
            _entities ??= _prevQuery.ToList();

            _context.Set<TEntity>().AttachRange(_entities);

            foreach (var entity in _entities)
            {
                var comp = Expression.Lambda<Func<TV>>(Expression.Invoke(update, Expression.Constant(entity))).Compile();
                _context.Entry(entity).Property(extract).CurrentValue = comp();
            }

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> SetIf<TV>(bool condition, Expression<Func<TEntity, TV>> extract, TV value)
        {
            if(condition)
            {
                if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
                _entities ??= _prevQuery.ToList();

                _context.Set<TEntity>().AttachRange(_entities);

                foreach (var entity in _entities)
                    _context.Entry(entity).Property(extract).CurrentValue = value;
            }

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> SetIf<TV>(bool condition, Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
        {
            if (condition)
            {
                if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
                _entities ??= _prevQuery.ToList();

                _context.Set<TEntity>().AttachRange(_entities);

                foreach (var entity in _entities)
                {
                    var comp = Expression.Lambda<Func<TV>>(Expression.Invoke(value)).Compile();
                    _context.Entry(entity).Property(extract).CurrentValue = comp();
                } 
            }

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> SetIf<TV>(bool condition, Expression<Func<TEntity, TV>> extract, Expression<Func<TEntity, TV>> update)
        {
            if (condition)
            {
                if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");
                _entities ??= _prevQuery.ToList();

                _context.Set<TEntity>().AttachRange(_entities);

                foreach (var entity in _entities)
                {
                    var comp = Expression.Lambda<Func<TV>>(Expression.Invoke(update, Expression.Constant(entity))).Compile();
                    _context.Entry(entity).Property(extract).CurrentValue = comp();
                } 
            }

            return this;
        }
    }
}
