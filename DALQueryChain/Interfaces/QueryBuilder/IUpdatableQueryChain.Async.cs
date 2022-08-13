namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Async
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task UpdateAsync(TEntity entity);
        public Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    }
}
