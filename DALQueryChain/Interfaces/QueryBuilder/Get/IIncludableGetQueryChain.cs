namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IIncludableGetQueryChain<T, TPreviousProperty> : IFilterableQueryChain<T>
        where T : class
    {
        IQueryable<T> Query { get; }
    }
}
