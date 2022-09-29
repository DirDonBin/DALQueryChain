namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableSetterQueryChain<TEntity> where TEntity : class, IDbModelBase
    {
        public void Update();
    }
}
