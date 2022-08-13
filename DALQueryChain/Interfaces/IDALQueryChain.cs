using DALQueryChain.Interfaces.QueryBuilder;

namespace DALQueryChain.Interfaces
{
    public interface IDALQueryChain<TContext>
        where TContext : class
    {
        public IQueryBuilder<TEntity> For<TEntity>() where TEntity : class, IDbModelBase;
    }
}
