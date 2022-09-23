namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task UpdateAsync(TEntity entity, CancellationToken ctn = default);
        public Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default);
        public Task UpdateAsync(CancellationToken ctn = default);
    }
}
