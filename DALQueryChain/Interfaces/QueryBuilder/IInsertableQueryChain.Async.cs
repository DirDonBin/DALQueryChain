namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IInsertableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task InsertAsync(TEntity entity, CancellationToken ctn = default);
        public Task<TEntity> InsertWithObjectAsync(TEntity entity, CancellationToken ctn = default);
        public Task BulkInsertAsync(IEnumerable<TEntity> entity, CancellationToken ctn = default);
    }
}
