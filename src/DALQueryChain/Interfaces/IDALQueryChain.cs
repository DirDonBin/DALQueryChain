using DALQueryChain.Interfaces.QueryBuilder;

namespace DALQueryChain.Interfaces
{
    public interface IDALQueryChain<TContext>
        where TContext : class
    {
        public IQueryBuilder<TEntity> For<TEntity>() where TEntity : class, IDbModelBase;

        void Transaction(Action<IDALQueryChain<TContext>> operation);
        Task TransactionAsync(Func<IDALQueryChain<TContext>, CancellationToken, Task> operation, CancellationToken ctn = default);

        TRepository Repository<TRepository>();
    }
}
