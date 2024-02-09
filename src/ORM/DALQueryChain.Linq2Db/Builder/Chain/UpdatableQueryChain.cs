using DALQueryChain.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB.Data;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : UpdatableSetterQueryChain<TContext, TEntity>, IUpdatableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _repository;
        private readonly TContext _context;

        public UpdatableQueryChain(TContext context, BaseRepository<TContext, TEntity> repository, IQueryable<TEntity> prevQuery)
            : base(context, repository, prevQuery)
        {
            _repository = repository;
            _context = context;

            _repository.IsBeforeTriggerOn = true;
            _repository.IsAfterTriggerOn = true;
        }

        public IUpdateFilterableQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _prevQuery = _prevQuery.Where(predicate);

            return this;
        }

        public IUpdateFilterableQueryChain<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            if (condition)
                _prevQuery = _prevQuery.Where(predicate);

            return this;
        }

        public IUpdateFilterableQueryChain<TEntity> When(bool condition, Func<IUpdateFilterableQueryChain<TEntity>, IUpdateFilterableQueryChain<TEntity>> query)
        {
            if (condition) return query(this);

            return this;
        }

        public IUpdatableQueryChain<TEntity> WithoutTriggers(TriggerType trigger = TriggerType.All)
        {
            _repository.IsBeforeTriggerOn = trigger is not TriggerType.All and not TriggerType.Before;
            _repository.IsAfterTriggerOn = trigger is not TriggerType.All and not TriggerType.After;

            return this;
        }
    }
}
