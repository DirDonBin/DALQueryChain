namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IIncludableGetQueryChain<T, out TPreviousProperty> : IFilterableQueryChain<T>
        where TPreviousProperty : class
    {
    }
}
