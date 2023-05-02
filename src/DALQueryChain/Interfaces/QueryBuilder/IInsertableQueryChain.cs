
namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IInsertableQueryChain<TEntity> : IChainSettings<IInsertableQueryChain<TEntity>>
        where TEntity : class, IDbModelBase
    {
    }
}
