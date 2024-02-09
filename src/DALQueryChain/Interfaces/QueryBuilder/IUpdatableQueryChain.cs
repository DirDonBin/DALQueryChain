using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.ComponentModel;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableQueryChain<TEntity> : IChainSettings<IUpdatableQueryChain<TEntity>>, IUpdateFilterableQueryChain<TEntity>
        where TEntity : class, IDbModelBase
    {
        
    }
}
