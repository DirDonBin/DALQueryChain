﻿using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public interface IQueryBuilder<TEntity> where TEntity : class, IDbModelBase
    {
        public IFilterableQueryChain<TEntity> Get { get; }
        public IInsertableQueryChain<TEntity> Insert { get; }
        public IUpdatableQueryChain<TEntity> Update { get; }
        public IDeletableQueryChain<TEntity> Delete { get; }
    }


}
