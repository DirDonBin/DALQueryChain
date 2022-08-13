﻿using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class DeletableQueryChain<TContext, TEntity> : IDeletableQueryChain<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private BaseRepository<TContext, TEntity> _repository;
        private TContext _context;

        public DeletableQueryChain(TContext context, BaseRepository<TContext, TEntity> repository)
        {
            _repository = repository;
            _context = context;
        }
    }
}
