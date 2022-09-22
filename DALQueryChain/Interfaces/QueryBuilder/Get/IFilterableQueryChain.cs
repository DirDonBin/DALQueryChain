using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IFilterableQueryChain<T> : IExecutableQueryChain<T>, ISelectableQueryChain<T>
    {
        //public IIncludableGetQueryChain<T, TProperty> LoadWith<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : class;

        IQueryable<T> Query { get; }

        public IOrderableQueryChain<T> OrderBy(Expression<Func<T, object>> selector);
        public IOrderableQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector);

        public IFilterableQueryChain<T> Where(Expression<Func<T, bool>> predicate);
        public IFilterableQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate);

        public IFilterableQueryChain<T> Skip(int count);
        public IFilterableQueryChain<T> SkipWhile(Expression<Func<T, bool>> predicate);
        public IFilterableQueryChain<T> Take(int count);
        public IFilterableQueryChain<T> TakeWhile(Expression<Func<T, bool>> predicate);

        public IFilterableQueryChain<T[]> Chunk(int count);
    }
}
