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
        private readonly BaseRepository<TContext, TEntity> _repository;
        private readonly IQueryable<TEntity>? _prevQuery;
        private IUpdatable<TEntity>? _prevUpdateQuery;

        public UpdatableSetterQueryChain(BaseRepository<TContext, TEntity> repository, IQueryable<TEntity>? prevQuery)
        {
            _repository = repository;
            _prevQuery = prevQuery;
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
    }
}
