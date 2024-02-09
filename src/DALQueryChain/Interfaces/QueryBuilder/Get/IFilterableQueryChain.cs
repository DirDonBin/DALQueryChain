using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IFilterableQueryChain<T> : IExecutableQueryChain<T>, ISelectableQueryChain<T>
    {
        //public IIncludableGetQueryChain<T, TProperty> LoadWith<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : class;

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupByAsEnumerable<TKey>(Expression<Func<T, TKey>> keySelector);
        public IFilterableQueryChain<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector);

        public IOrderableQueryChain<T> OrderBy(Expression<Func<T, object>> selector);
        public IOrderableQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector);

        public IFilterableQueryChain<T> Where(Expression<Func<T, bool>> predicate);
        public IFilterableQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate);

        public IFilterableQueryChain<T> When(bool condition, Func<IFilterableQueryChain<T>, IFilterableQueryChain<T>> query);

        public IFilterableQueryChain<T> Skip(int count);
        public IFilterableQueryChain<T> SkipWhile(Expression<Func<T, bool>> predicate);
        public IFilterableQueryChain<T> Take(int count);
        public IFilterableQueryChain<T> TakeWhile(Expression<Func<T, bool>> predicate);

        public IFilterableQueryChain<T[]> Chunk(int count);

        public IFilterableQueryChain<T> Concat(IFilterableQueryChain<T> second);
        public IFilterableQueryChain<T> Union(IFilterableQueryChain<T> second);
        public IFilterableQueryChain<T> UnionBy<TKey>(IFilterableQueryChain<T> second, Expression<Func<T, TKey>> keySelector);
        public IFilterableQueryChain<T> Except(IFilterableQueryChain<T> second);
        public IFilterableQueryChain<T> ExceptBy(IFilterableQueryChain<T> second, Expression<Func<T, T>> keySelector);

        public IFilterableQueryChain<T> Reverse();
    }
}
