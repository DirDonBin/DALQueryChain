namespace DALQueryChain.Linq2Db.Builder.Chain.Get
{
    internal abstract class BaseGetQueryChain<T>
    {
        protected IQueryable<T> _prevQuery;

        protected BaseGetQueryChain(IQueryable<T> prevQuery)
        {
            _prevQuery = prevQuery;
        }
    }
}
