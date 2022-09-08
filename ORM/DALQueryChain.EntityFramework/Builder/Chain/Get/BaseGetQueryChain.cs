namespace DALQueryChain.EntityFramework.Builder.Chain.Get
{
    internal abstract class BaseGetQueryChain<T>
        where T : class
    {
        protected IQueryable<T> _prevQuery;

        protected BaseGetQueryChain(IQueryable<T> prevQuery)
        {
            _prevQuery = prevQuery;
        }
    }
}
