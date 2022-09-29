namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableSetterQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public Task UpdateAsync(CancellationToken ctn = default);
    }
}
