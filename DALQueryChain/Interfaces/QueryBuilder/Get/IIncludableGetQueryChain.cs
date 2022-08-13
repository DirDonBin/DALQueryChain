using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IIncludableGetQueryChain<T, TPreviousProperty> : IFilterableQueryChain<T>
        where T : class
    {
        public IIncludableGetQueryChain<T, IEnumerable<TProperty>> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> selector);
        public IIncludableGetQueryChain<T, TProperty> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, TProperty>> selector);
    }
}
