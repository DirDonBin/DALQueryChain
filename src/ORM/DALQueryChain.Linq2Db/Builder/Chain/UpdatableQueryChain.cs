using DALQueryChain.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class UpdatableQueryChain<TContext, TEntity> : IUpdatableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _repository;
        private readonly TContext _context;

        public UpdatableQueryChain(TContext context, BaseRepository<TContext, TEntity> repository)
        {
            _repository = repository;
            _context = context;

            _repository.IsBeforeTriggerOn = true;
            _repository.IsAfterTriggerOn = true;
        }

        public IUpdatableSetterQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
            => new UpdatableSetterQueryChain<TContext, TEntity>(_repository, _context.GetTable<TEntity>().Where(predicate));

        public IUpdatableQueryChain<TEntity> WithoutTriggers(TriggerType trigger = TriggerType.All)
        {
            _repository.IsBeforeTriggerOn = trigger is not TriggerType.All and not TriggerType.Before;
            _repository.IsAfterTriggerOn = trigger is not TriggerType.All and not TriggerType.After;

            return this;
        }
    }
}
