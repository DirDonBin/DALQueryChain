namespace DALQueryChain.Interfaces.QueryBuilder
{
    //Sync
    public partial interface IInsertableQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public void Insert(TEntity entity);
        public TEntity InsertWithObject(TEntity entity);
        public void BulkInsert(IEnumerable<TEntity> entity);
    }
}
