using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
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

        public T Last() => _prevQuery.Last();

        public T Last(Expression<Func<T, bool>> predicate) => _prevQuery.Last(predicate);

        public T Last<TKey>(Expression<Func<T, TKey>> keySelector) => _prevQuery.OrderByDescending(keySelector).First();

        public T Last<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector) => _prevQuery.OrderByDescending(keySelector).First(predicate);

        public T? LastOrDefault() => _prevQuery.LastOrDefault();

        public T? LastOrDefault(Expression<Func<T, bool>> predicate) => _prevQuery.LastOrDefault(predicate);

        public T? LastOrDefault<TKey>(Expression<Func<T, TKey>> keySelector) => _prevQuery.OrderByDescending(keySelector).FirstOrDefault();

        public T? LastOrDefault<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector) => _prevQuery.OrderByDescending(keySelector).FirstOrDefault(predicate);

        public List<T> ToList() => _prevQuery.ToList();

        public T? Max() => _prevQuery.Max();

        public TResult? Max<TResult>(Expression<Func<T, TResult>> predicate) => _prevQuery.Max(predicate);

        public T? MaxBy<TKey>(Expression<Func<T, TKey>> keySelector) => _prevQuery.MaxBy(keySelector);

        public T? Min() => _prevQuery.Min();

        public TResult? Min<TResult>(Expression<Func<T, TResult>> predicate) => _prevQuery.Min(predicate);

        public T? MinBy<TKey>(Expression<Func<T, TKey>> keySelector) => _prevQuery.MinBy(keySelector);
    }
}
