﻿using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
        where T : class
    {
        public FilterableQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IFilterableQueryChain<T[]> Chunk(int count)
        {
            return new FilterableQueryChain<T[]>(_prevQuery.Chunk(count));
        }

        public IIncludableGetQueryChain<T, TProperty> LoadWith<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            return new IncludableGetQueryChain<T, TProperty>(_prevQuery.Include(selector));
        }

        public IOrderableQueryChain<T> OrderBy(Expression<Func<T, object>> selector)
        {
            return new OrderableQueryChain<T>(_prevQuery.OrderBy(selector));
        }

        public IOrderableQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector)
        {
            return new OrderableQueryChain<T>(_prevQuery.OrderByDescending(selector));
        }

        public IFilterableQueryChain<T> Skip(int count)
        {
            _prevQuery = _prevQuery.Skip(count);
            return this;
        }

        //public IFilterableQueryChain<T> Skip(Expression<Func<int>> selector)
        //{
        //    _prevQuery = _prevQuery.Skip(selector.);
        //    return this;
        //}

        public IFilterableQueryChain<T> Take(int count)
        {
            _prevQuery = _prevQuery.Take(count);
            return this;
        }

        //public IFilterableQueryChain<T> Take(Expression<Func<int>> selector)
        //{
        //    _prevQuery = _prevQuery.Take(selector.);
        //    return this;
        //}

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
    }
}
