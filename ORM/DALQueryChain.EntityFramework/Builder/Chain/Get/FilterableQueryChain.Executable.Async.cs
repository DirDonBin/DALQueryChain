using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
    {
        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => await _prevQuery.AllAsync(predicate, ctn);

        public async Task<bool> AnyAsync(CancellationToken ctn = default) => await _prevQuery.AnyAsync(ctn);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => await _prevQuery.AnyAsync(predicate, ctn);

        public async Task<int> CountAsync(CancellationToken ctn = default) => await _prevQuery.CountAsync(ctn);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => await _prevQuery.CountAsync(predicate, ctn);

        public async Task<T> FirstAsync(CancellationToken ctn = default) => await _prevQuery.FirstAsync(ctn);

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => await _prevQuery.FirstAsync(predicate, ctn);

        public async Task<T?> FirstOrDefaultAsync(CancellationToken ctn = default) => await _prevQuery.FirstOrDefaultAsync(ctn);

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => await _prevQuery.FirstOrDefaultAsync(predicate, ctn);

        public async Task<T?> SingleOrDefaultAsync(CancellationToken ctn = default) => await _prevQuery.SingleOrDefaultAsync(ctn);

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => await _prevQuery.SingleOrDefaultAsync(predicate, ctn);

        public Task<T> LastAsync(CancellationToken ctn = default) => _prevQuery.LastAsync(ctn);

        public Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.LastAsync(predicate, ctn);

        public Task<T> LastAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstAsync(ctn);

        public Task<T> LastAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstAsync(predicate, ctn);

        public Task<T?> LastOrDefaultAsync(CancellationToken ctn = default) => _prevQuery.LastOrDefaultAsync(ctn);

        public Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ctn = default) => _prevQuery.LastOrDefaultAsync(predicate, ctn);

        public Task<T?> LastOrDefaultAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstOrDefaultAsync(ctn);

        public Task<T?> LastOrDefaultAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstOrDefaultAsync(predicate, ctn);

        public async Task<List<T>> ToListAsync(CancellationToken ctn = default) => await _prevQuery.ToListAsync(ctn);

        public Task<T?> MaxAsync(CancellationToken ctn = default) => _prevQuery.MaxAsync(ctn)!;

        public Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default) => _prevQuery.MaxAsync(predicate, ctn)!;

        public Task<T?> MaxByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstOrDefaultAsync(ctn);

        public Task<T?> MinAsync(CancellationToken ctn = default) => _prevQuery.MinAsync(ctn)!;

        public Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default) => _prevQuery.MinAsync(predicate, ctn)!;

        public Task<T?> MinByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderBy(keySelector).FirstOrDefaultAsync(ctn);
    }
}
