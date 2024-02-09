using DALQueryChain.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableSetterQueryChain<TContext, TEntity> : IUpdatableSetterQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly TContext _context;
        private readonly BaseRepository<TContext, TEntity> _repository;
        protected IQueryable<TEntity> _prevQuery;
        private IUpdatable<TEntity>? _prevUpdateQuery;

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

            _prevUpdateQuery = true switch
            {
                { } when _prevUpdateQuery is null => _prevQuery.Set(extract, value),
                _ => _prevUpdateQuery!.Set(extract, value)
            };

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");

            _prevUpdateQuery = true switch
            {
                { } when _prevUpdateQuery is null => _prevQuery.Set(extract, value),
                _ => _prevUpdateQuery!.Set(extract, value)
            };

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TEntity, TV>> update)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");

            _prevUpdateQuery = true switch
            {
                { } when _prevUpdateQuery is null => _prevQuery.Set(extract, update),
                _ => _prevUpdateQuery!.Set(extract, update)
            };

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> SetIf<TV>(bool condition, Expression<Func<TEntity, TV>> extract, TV value)
        {
            if (condition)
            {
                if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");

                _prevUpdateQuery = true switch
                {
                    { } when _prevUpdateQuery is null => _prevQuery.Set(extract, value),
                    _ => _prevUpdateQuery!.Set(extract, value)
                }; 
            }

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> SetIf<TV>(bool condition, Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
        {
            if (condition)
            {
                if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");

                _prevUpdateQuery = true switch
                {
                    { } when _prevUpdateQuery is null => _prevQuery.Set(extract, value),
                    _ => _prevUpdateQuery!.Set(extract, value)
                }; 
            }

            return this;
        }

        public IUpdatableSetterQueryChain<TEntity> SetIf<TV>(bool condition, Expression<Func<TEntity, TV>> extract, Expression<Func<TEntity, TV>> update)
        {
            if (condition)
            {
                if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");

                _prevUpdateQuery = true switch
                {
                    { } when _prevUpdateQuery is null => _prevQuery.Set(extract, update),
                    _ => _prevUpdateQuery!.Set(extract, update)
                }; 
            }

            return this;
        }
    }
}
