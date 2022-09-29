﻿using DALQueryChain.EntityFramework.Repositories;
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

        public IUpdatableSetterQueryChain<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _entities = _context.Set<TEntity>().Where(predicate).ToList();
            return new UpdatableSetterQueryChain<TContext, TEntity>(_context, _repository);
        }
    }
}
