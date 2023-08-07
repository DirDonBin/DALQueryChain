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

        public Dictionary<TKey, T> ToDictionary<TKey>(Func<T, TKey> keySelector) where TKey : notnull
            => _prevQuery.ToDictionary(keySelector);

        public Dictionary<TKey, TResult> ToDictionary<TKey, TResult>(Func<T, TKey> keySelector, Func<T, TResult> valueSelector) where TKey : notnull
            => _prevQuery.ToDictionary(keySelector, valueSelector);

        public T Max() => _prevQuery.Max()!;
        public T? MaxOrDefault()
        {
            if (_prevQuery.Any()) return _prevQuery.Max();
            return default;
        }

        public TResult Max<TResult>(Expression<Func<T, TResult>> predicate) => _prevQuery.Max(predicate)!;
        public TResult? MaxOrDefault<TResult>(Expression<Func<T, TResult>> predicate)
        {
            if (_prevQuery.Any()) return _prevQuery.Max(predicate);
            return default;
        }

        public T MaxBy<TKey>(Expression<Func<T, TKey>> keySelector) => _prevQuery.MaxBy(keySelector)!;
        public T? MaxByOrDefault<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (_prevQuery.Any()) return _prevQuery.MaxBy(keySelector);
            return default;
        }

        public T Min() => _prevQuery.Min()!;
        public T? MinOrDefault()
        {
            if (_prevQuery.Any()) return _prevQuery.Min();
            return default;
        }

        public TResult Min<TResult>(Expression<Func<T, TResult>> predicate) => _prevQuery.Min(predicate)!;
        public TResult? MinOrDefault<TResult>(Expression<Func<T, TResult>> predicate)
        {
            if (_prevQuery.Any()) return _prevQuery.Min(predicate);
            return default;
        }

        public T MinBy<TKey>(Expression<Func<T, TKey>> keySelector) => _prevQuery.MinBy(keySelector)!;
        public T? MinByOrDefault<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (_prevQuery.Any()) return _prevQuery.MinBy(keySelector);
            return default;
        }

        public int Sum(Expression<Func<T, int>> keySelector) => _prevQuery.Sum(keySelector);

        public int? Sum(Expression<Func<T, int?>> keySelector) => _prevQuery.Sum(keySelector);

        public long Sum(Expression<Func<T, long>> keySelector) => _prevQuery.Sum(keySelector);

        public long? Sum(Expression<Func<T, long?>> keySelector) => _prevQuery.Sum(keySelector);

        public float Sum(Expression<Func<T, float>> keySelector) => _prevQuery.Sum(keySelector);

        public float? Sum(Expression<Func<T, float?>> keySelector) => _prevQuery.Sum(keySelector);

        public decimal Sum(Expression<Func<T, decimal>> keySelector) => _prevQuery.Sum(keySelector);

        public decimal? Sum(Expression<Func<T, decimal?>> keySelector) => _prevQuery.Sum(keySelector);

        public double Sum(Expression<Func<T, double>> keySelector) => _prevQuery.Sum(keySelector);

        public double? Sum(Expression<Func<T, double?>> keySelector) => _prevQuery.Sum(keySelector);
    }
}
