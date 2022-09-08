using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IFilterableQueryChain<T> : IExecutableQueryChain<T>, ISelectableQueryChain<T>
        where T : class
    {
        public IIncludableGetQueryChain<T, TProperty> LoadWith<TProperty>(Expression<Func<T, TProperty>> selector);

        public IOrderableQueryChain<T> OrderBy(Expression<Func<T, object>> selector);
        public IOrderableQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector);

        public IFilterableQueryChain<T> Where(Expression<Func<T, bool>> predicate);
        public IFilterableQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate);

        public IFilterableQueryChain<T> Skip(int count);
        public IFilterableQueryChain<T> Skip(Expression<Func<int>> selector);
        public IFilterableQueryChain<T> Take(int count);
        public IFilterableQueryChain<T> Take(Expression<Func<int>> selector);

        public IFilterableQueryChain<T[]> Chunk(int count);
    }
}
