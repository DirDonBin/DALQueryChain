using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;

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

            _repository.IsBeforeTriggerOn = true;
            _repository.IsAfterTriggerOn = true;
        }

        public IDeletableQueryChain<TEntity> WithoutTriggers(TriggerType trigger = TriggerType.All)
        {
            _repository.IsBeforeTriggerOn = trigger is not TriggerType.All and not TriggerType.Before;
            _repository.IsAfterTriggerOn = trigger is not TriggerType.All and not TriggerType.After;

            return this;
        }
    }
}
