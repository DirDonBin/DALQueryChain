namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IDeletableQueryChain<TEntity> : IChainSettings<IDeletableQueryChain<TEntity>>
        where TEntity : class, IDbModelBase
    {
    }
}
