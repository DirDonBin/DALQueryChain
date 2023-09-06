﻿using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {
        internal IQueryable<T> Query => _prevQuery;

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
            _prevQuery = _prevQuery.Skip(count);
            return this;
        }

        public IFilterableQueryChain<T> SkipWhile(Expression<Func<T, bool>> predicate)
        {
            _prevQuery = _prevQuery.SkipWhile(predicate);
            return this;
        }

        public IFilterableQueryChain<T> Take(int count)
        {
            _prevQuery = _prevQuery.Take(count);
            return this;
        }

        public IFilterableQueryChain<T> TakeWhile(Expression<Func<T, bool>> predicate)
        {
            _prevQuery = _prevQuery.TakeWhile(predicate);
            return this;
        }

        public IFilterableQueryChain<T> Where(Expression<Func<T, bool>> predicate)
        {
            _prevQuery = _prevQuery.Where(predicate);
            return this;
        }

        public IFilterableQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                _prevQuery = _prevQuery.Where(predicate);

            return this;
        }

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupByAsEnumerable<TKey>(Expression<Func<T, TKey>> keySelector)
             => new FilterableQueryChain<IGrouping<TKey, T>>(Enumerable.GroupBy(_prevQuery.AsEnumerable(), keySelector.Compile()).AsQueryable());

        public IFilterableQueryChain<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector)
            => new FilterableQueryChain<IGrouping<TKey, T>>(_prevQuery.GroupBy(keySelector));

        public IFilterableQueryChain<T> Concat(IFilterableQueryChain<T> second)
        {
            _prevQuery = _prevQuery.Concat(((FilterableQueryChain<T>)second).Query);
            return this;
        }

        public IFilterableQueryChain<T> Union(IFilterableQueryChain<T> second)
        {
            _prevQuery = _prevQuery.Union(((FilterableQueryChain<T>)second).Query);
            return this;
        }

        public IFilterableQueryChain<T> UnionBy<TKey>(IFilterableQueryChain<T> second, Expression<Func<T, TKey>> keySelector)
        {
            _prevQuery = _prevQuery.UnionBy(((FilterableQueryChain<T>)second).Query, keySelector);
            return this;
        }

        public IFilterableQueryChain<T> Except(IFilterableQueryChain<T> second)
        {
            _prevQuery = _prevQuery.Except(((FilterableQueryChain<T>)second).Query);
            return this;
        }

        public IFilterableQueryChain<T> ExceptBy(IFilterableQueryChain<T> second, Expression<Func<T, T>> keySelector)
        {
            _prevQuery = _prevQuery.ExceptBy(((FilterableQueryChain<T>)second).Query, keySelector);
            return this;
        }
    }
}
