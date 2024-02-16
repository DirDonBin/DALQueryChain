using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.EntityFramework.Extensions;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {
        internal IQueryable<T> Query => _prevQuery;
        internal Expression<Func<IQueryable<T>, IQueryable<T>>> LoadFunc;

        public FilterableQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IFilterableQueryChain<T[]> Chunk(int count)
            => new FilterableQueryChain<T[]>(_prevQuery.Chunk(count));

        public IOrderableQueryChain<T> OrderBy(Expression<Func<T, object>> selector)
            => new OrderableQueryChain<T>(_prevQuery.OrderBy(selector));

        public IOrderableQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector)
            => new OrderableQueryChain<T>(_prevQuery.OrderByDescending(selector));

        public IFilterableQueryChain<T> Skip(int count)
        {
            QueryApply(q => q.Skip(count));
            return this;
        }

        public IFilterableQueryChain<T> SkipWhile(Expression<Func<T, bool>> predicate)
        {
            QueryApply(q => q.SkipWhile(predicate));
            return this;
        }

        public IFilterableQueryChain<T> Take(int count)
        {
            QueryApply(q => q.Take(count));
            return this;
        }

        public IFilterableQueryChain<T> TakeWhile(Expression<Func<T, bool>> predicate)
        {
            QueryApply(q => q.TakeWhile(predicate));
            return this;
        }

        public IFilterableQueryChain<T> Where(Expression<Func<T, bool>> predicate)
        {
            QueryApply(q => q.Where(predicate));
            return this;
        }

        public IFilterableQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                QueryApply(q => q.Where(predicate));

            return this;
        }

        public IFilterableQueryChain<T> When(bool condition, Func<IFilterableQueryChain<T>, IFilterableQueryChain<T>> query)
        {
            if (condition) return query(this);

            return this;
        }

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupByAsEnumerable<TKey>(Expression<Func<T, TKey>> keySelector)
             => new FilterableQueryChain<IGrouping<TKey, T>>(Enumerable.GroupBy(_prevQuery.AsEnumerable(), keySelector.Compile()).AsQueryable());

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector)
            => new FilterableQueryChain<IGrouping<TKey, T>>(_prevQuery.GroupBy(keySelector));

        public IFilterableQueryChain<T> Concat(IFilterableQueryChain<T> second)
        {
            QueryApply(q => q.Concat(((FilterableQueryChain<T>)second).Query));
            return this;
        }

        public IFilterableQueryChain<T> Union(IFilterableQueryChain<T> second)
        {
            QueryApply(q => q.Union(((FilterableQueryChain<T>)second).Query));
            return this;
        }

        public IFilterableQueryChain<T> UnionBy<TKey>(IFilterableQueryChain<T> second, Expression<Func<T, TKey>> keySelector)
        {
            QueryApply(q => q.UnionBy(((FilterableQueryChain<T>)second).Query, keySelector));
            return this;
        }

        public IFilterableQueryChain<T> Except(IFilterableQueryChain<T> second)
        {
            QueryApply(q => q.Except(((FilterableQueryChain<T>)second).Query));
            return this;
        }

        public IFilterableQueryChain<T> ExceptBy(IFilterableQueryChain<T> second, Expression<Func<T, T>> keySelector)
        {
            QueryApply(q => q.ExceptBy(((FilterableQueryChain<T>)second).Query, keySelector));
            return this;
        }

        public IFilterableQueryChain<T> Reverse()
        {
            Expression<Func<IQueryable<T>, IQueryable<T>>> func = q
                => q.AsSubQuery().OrderByDescending(x => Sql.Ext.RowNumber().Over().ToValue()).Skip(0);

            QueryApply(func);
            return this;
        }

        private void QueryApply(Expression<Func<IQueryable<T>, IQueryable<T>>> func)
        {
            _prevQuery = func.Compile()(_prevQuery);

            LoadFunc = q => func.Compile()(LoadFunc.Compile()(q));
        }
    }
}
