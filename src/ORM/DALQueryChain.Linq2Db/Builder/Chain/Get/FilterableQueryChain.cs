using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {

        internal IQueryable<T> Query => _prevQuery;
        internal Expression<Func<IQueryable<T>, IQueryable<T>>> LoadFunc;

        public FilterableQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
            LoadFunc = q => prevQuery;
        }

        public IFilterableQueryChain<T[]> Chunk(int count)
            => new FilterableQueryChain<T[]>(_prevQuery.Chunk(count));

        public IOrderableQueryChain<T> OrderBy(Expression<Func<T, object>> selector)
            => new OrderableQueryChain<T>(_prevQuery.OrderBy(selector));

        public IOrderableQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector)
            => new OrderableQueryChain<T>(_prevQuery.OrderByDescending(selector));


        public IFilterableQueryChain<T> Skip(int count)
        {
            return QueryApply(q => q.Skip(count));
        }

        public IFilterableQueryChain<T> SkipWhile(Expression<Func<T, bool>> predicate)
        {
            return QueryApply(q => q.SkipWhile(predicate));
        }

        public IFilterableQueryChain<T> Take(int count)
        {
            return QueryApply(q => q.Take(count));
        }

        public IFilterableQueryChain<T> TakeWhile(Expression<Func<T, bool>> predicate)
        {
            return QueryApply(q => q.TakeWhile(predicate));
        }

        public IFilterableQueryChain<T> Where(Expression<Func<T, bool>> predicate)
        {
            return QueryApply(q => q.Where(predicate));
        }

        public IFilterableQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                return QueryApply(q => q.Where(predicate));

            return QueryApply(q => Query);
        }

        public IFilterableQueryChain<T> When(bool condition, Func<IFilterableQueryChain<T>, IFilterableQueryChain<T>> query)
        {
            if (condition) return query(QueryApply(q => Query));

            return QueryApply(q => Query);
        }

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupByAsEnumerable<TKey>(Expression<Func<T, TKey>> keySelector)
             => new FilterableQueryChain<IGrouping<TKey, T>>(Enumerable.GroupBy(_prevQuery.AsEnumerable(), keySelector.Compile()).AsQueryable());

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector)
            => new FilterableQueryChain<IGrouping<TKey, T>>(_prevQuery.GroupBy(keySelector));

        public IFilterableQueryChain<T> Concat(IFilterableQueryChain<T> second)
        {
            return QueryApply(q => q.Concat(((FilterableQueryChain<T>)second).Query));
        }

        public IFilterableQueryChain<T> Union(IFilterableQueryChain<T> second)
        {
            return QueryApply(q => q.Union(((FilterableQueryChain<T>)second).Query));
        }

        public IFilterableQueryChain<T> UnionBy<TKey>(IFilterableQueryChain<T> second, Expression<Func<T, TKey>> keySelector)
        {
            return QueryApply(q => q.UnionBy(((FilterableQueryChain<T>)second).Query, keySelector));
        }

        public IFilterableQueryChain<T> Except(IFilterableQueryChain<T> second)
        {
            return QueryApply(q => q.Except(((FilterableQueryChain<T>)second).Query));
        }

        public IFilterableQueryChain<T> ExceptBy(IFilterableQueryChain<T> second, Expression<Func<T, T>> keySelector)
        {
            return QueryApply(q => q.ExceptBy(((FilterableQueryChain<T>)second).Query, keySelector));
        }

        public IFilterableQueryChain<T> Reverse()
        {
            Expression<Func<IQueryable<T>, IQueryable<T>>> func = q
                => q.AsSubQuery().OrderByDescending(x => Sql.Ext.RowNumber().Over().ToValue()).Skip(0);

            return QueryApply(func);
        }

        private IFilterableQueryChain<T> QueryApply(Expression<Func<IQueryable<T>, IQueryable<T>>> func)
        {
            return new FilterableQueryChain<T>(func.Compile()(Query));
        }


    }


}
