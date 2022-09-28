using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder
{
    internal class QueryBuilderChain<TContext, TEntity> : BaseQueryChain<TContext>, IQueryBuilder<TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private readonly BaseRepository<TContext, TEntity> _baseRepository;

        IFilterableQueryChain<TEntity> IQueryBuilder<TEntity>.Get => new FilterableQueryChain<TEntity>(_context.GetTable<TEntity>());

        IInsertableQueryChain<TEntity> IQueryBuilder<TEntity>.Insert => new InsertableQueryChain<TContext, TEntity>(_context, _baseRepository);

        IUpdatableQueryChain<TEntity> IQueryBuilder<TEntity>.Update => new UpdatableQueryChain<TContext, TEntity>(_context, _baseRepository);

        IDeletableQueryChain<TEntity> IQueryBuilder<TEntity>.Delete => new DeletableQueryChain<TContext, TEntity>(_context, _baseRepository);

        internal QueryBuilderChain(TContext context, IServiceProvider serviceProvider, IDALQueryChain<TContext>? defQC = null) : base(context, serviceProvider, defQC)
        {
            _baseRepository = GetGenericRepository<TEntity>();
        }

        public TRepository? Repository<TRepository>() where TRepository : IRepository
        {
            return GetRepository<TRepository, TEntity>();
        }
    }
}
