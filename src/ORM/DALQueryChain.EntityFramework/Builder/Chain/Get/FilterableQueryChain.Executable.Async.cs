﻿using DALQueryChain.EntityFramework.Builder.Chain.Get;
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

        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, CancellationToken ctn = default) where TKey : notnull
            => _prevQuery.ToDictionaryAsync(keySelector, ctn);

        public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<T, TKey> keySelector, Func<T, TResult> valueSelector, CancellationToken ctn = default) where TKey : notnull
            => _prevQuery.ToDictionaryAsync(keySelector, valueSelector, ctn);

        public Task<T> MaxAsync(CancellationToken ctn = default) => _prevQuery.MaxAsync(ctn);
        public async Task<T?> MaxOrDefaultAsync(CancellationToken ctn = default)
        {
            if (await _prevQuery.AnyAsync(ctn)) return await _prevQuery.MaxAsync(ctn);
            return default;
        }

        public Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default) => _prevQuery.MaxAsync(predicate, ctn);
        public async Task<TResult?> MaxOrDefaultAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default)
        {
            if (await _prevQuery.AnyAsync(ctn)) return await _prevQuery.MaxAsync(predicate, ctn);
            return default;
        }

        public Task<T> MaxByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderByDescending(keySelector).FirstAsync(ctn);
        public async Task<T?> MaxByOrDefaultAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default)
        {
            if (await _prevQuery.AnyAsync(ctn)) return await _prevQuery.OrderByDescending(keySelector).FirstAsync(ctn);
            return default;
        }

        public Task<T> MinAsync(CancellationToken ctn = default) => _prevQuery.MinAsync(ctn)!;
        public async Task<T?> MinOrDefaultAsync(CancellationToken ctn = default)
        {
            if (await _prevQuery.AnyAsync(ctn)) return await _prevQuery.MinAsync(ctn);
            return default;
        }

        public Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default) => _prevQuery.MinAsync(predicate, ctn)!;
        public async Task<TResult?> MinOrDefaultAsync<TResult>(Expression<Func<T, TResult>> predicate, CancellationToken ctn = default)
        {
            if (await _prevQuery.AnyAsync(ctn)) return await _prevQuery.MinAsync(predicate, ctn);
            return default;
        }

        public Task<T> MinByAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default) => _prevQuery.OrderBy(keySelector).FirstAsync(ctn);
        public async Task<T?> MinByOrDefaultAsync<TKey>(Expression<Func<T, TKey>> keySelector, CancellationToken ctn = default)
        {
            if (await _prevQuery.AnyAsync(ctn)) return await _prevQuery.OrderByDescending(keySelector).FirstAsync(ctn);
            return default;
        }
    }
}
