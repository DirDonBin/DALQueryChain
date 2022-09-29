using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {

        IQueryable<T> IFilterableQueryChain<T>.Query => _prevQuery;

        public FilterableQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IFilterableQueryChain<T[]> Chunk(int count)
        {
            return new FilterableQueryChain<T[]>(_prevQuery.Chunk(count));
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
    }
}
