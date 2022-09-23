using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _repository;
        private readonly TContext _context;
        private IQueryable<TEntity>? _prevQuery = null;
        private IUpdatable<TEntity>? _prevUpdateQuery = null;

        public UpdatableQueryChain(TContext context, BaseRepository<TContext, TEntity> repository)
        {
            _repository = repository;
            _context = context;
        }

        public IUpdatableQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _prevQuery = _context.GetTable<TEntity>().Where(predicate);
            return this;
        }

        public IUpdatableQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, TV value)
        {
            if (_prevQuery is null) throw new InvalidOperationException("Has not been used of method Where");

            _prevUpdateQuery = true switch
            {
                { } when _prevUpdateQuery is null => _prevQuery.Set(extract, value),
                _ => _prevUpdateQuery!.Set(extract, value)
            };

            return this;
        }

        public IUpdatableQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value)
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
