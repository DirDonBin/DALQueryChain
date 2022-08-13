﻿using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
        where T : class
    {
        public bool All(Expression<Func<T, bool>> predicate) => _prevQuery.All(predicate);

        public bool Any() => _prevQuery.Any();

        public bool Any(Expression<Func<T, bool>> predicate) => _prevQuery.Any(predicate);

        public int Count() => _prevQuery.Count();

        public int Count(Expression<Func<T, bool>> predicate) => _prevQuery.Count(predicate);

        public T First() => _prevQuery.First();

        public T First(Expression<Func<T, bool>> predicate) => _prevQuery.First(predicate);

        public T? FirstOrDefault() => _prevQuery.FirstOrDefault();

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate) => _prevQuery.FirstOrDefault(predicate);

        public T? SingleOrDefault() => _prevQuery.SingleOrDefault();

        public T? SingleOrDefault(Expression<Func<T, bool>> predicate) => _prevQuery.SingleOrDefault(predicate);

        public List<T> ToList() => _prevQuery.ToList();
    }
}
