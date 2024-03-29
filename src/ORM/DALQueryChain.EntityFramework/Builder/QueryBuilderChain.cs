﻿using DALQueryChain.EntityFramework.Builder.Chain;
using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder
{
    internal class QueryBuilderChain<TContext, TEntity> : BaseQueryChain<TContext>, IQueryBuilder<TEntity>
        where TContext : DbContext
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _baseRepository;

        IFilterableQueryChain<TEntity> IQueryBuilder<TEntity>.Get => new FilterableQueryChain<TEntity>(_context.Set<TEntity>().AsNoTracking());

        IInsertableQueryChain<TEntity> IQueryBuilder<TEntity>.Insert => new InsertableQueryChain<TContext, TEntity>(_context, _baseRepository);

        IUpdatableQueryChain<TEntity> IQueryBuilder<TEntity>.Update => new UpdatableQueryChain<TContext, TEntity>(_context, _baseRepository, _context.Set<TEntity>());

        IDeletableQueryChain<TEntity> IQueryBuilder<TEntity>.Delete => new DeletableQueryChain<TContext, TEntity>(_context, _baseRepository);



        internal QueryBuilderChain(TContext context, IDALQueryChain<TContext> defQC) : base(context, defQC)
        {
            _baseRepository = ((BuildQuery<TContext>)_defQC).GetGenericRepository<TEntity>();
        }
    }
}
