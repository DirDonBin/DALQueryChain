using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {
        public Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.AllAsync(predicate, ctn);

        public Task<bool> AnyAsync(CancellationToken ctn = default) => _prevQuery.AnyAsync(ctn);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.AnyAsync(predicate, ctn);

        public Task<int> CountAsync(CancellationToken ctn = default) => _prevQuery.CountAsync(ctn);

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.CountAsync(predicate, ctn);

        public Task<T> FirstAsync(CancellationToken ctn = default) => _prevQuery.FirstAsync(ctn);

        public Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.FirstAsync(predicate, ctn);

        public Task<T?> FirstOrDefaultAsync(CancellationToken ctn = default) => _prevQuery.FirstOrDefaultAsync(ctn);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.FirstOrDefaultAsync(predicate, ctn);

        public Task<T?> SingleOrDefaultAsync(CancellationToken ctn = default) => _prevQuery.SingleOrDefaultAsync(ctn);

        public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.SingleOrDefaultAsync(predicate, ctn);

        public Task<T> LastAsync(CancellationToken ctn = default) => _prevQuery.OrderByDescending(x => x).FirstAsync(ctn);

        public Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.OrderByDescending(x => x).FirstAsync(predicate, ctn);

        public Task<T> LastAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstAsync(ctn);

        public Task<T> LastAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstAsync(predicate, ctn);

        public Task<T?> LastOrDefaultAsync(CancellationToken ctn = default) => _prevQuery.OrderByDescending(x => x).FirstOrDefaultAsync(ctn);

        public Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.OrderByDescending(x => x).FirstOrDefaultAsync(predicate, ctn);

        public Task<T?> LastOrDefaultAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstOrDefaultAsync(ctn);

        public Task<T?> LastOrDefaultAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstOrDefaultAsync(predicate, ctn);

        public Task<List<T>> ToListAsync(CancellationToken ctn = default) => _prevQuery.ToListAsync(ctn);

        public Task<T?> MaxAsync(CancellationToken ctn = default) => _prevQuery.MaxAsync(ctn);

        public Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default) => _prevQuery.MaxAsync(predicate, ctn);

        public Task<T?> MaxByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstOrDefaultAsync(ctn);

        public Task<T?> MinAsync(CancellationToken ctn = default) => _prevQuery.MinAsync(ctn);

        public Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default) => _prevQuery.MinAsync(predicate, ctn);

        public Task<T?> MinByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderBy(keySelector).FirstOrDefaultAsync(ctn);
    }
}
