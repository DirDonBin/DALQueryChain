namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Sync
    public partial interface IUpdatableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public void Update(TEntity entity);
        public void BulkUpdate(IEnumerable<TEntity> entities);
    }
}
