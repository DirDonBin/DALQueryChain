using DALQueryChain.Linq2Db.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
        where T : class
    {
        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate) => await _prevQuery.AllAsync(predicate);

        public async Task<bool> AnyAsync() => await _prevQuery.AnyAsync();

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await _prevQuery.AnyAsync(predicate);

        public async Task<int> CountAsync() => await _prevQuery.CountAsync();

        async Task<int> IExecutableQueryChain<T>.CountAsync(Expression<Func<T, bool>> predicate) => await _prevQuery.CountAsync(predicate);

        public async Task<T> FirstAsync() => await _prevQuery.FirstAsync();

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate) => await _prevQuery.FirstAsync(predicate);

        public async Task<T?> FirstOrDefaultAsync() => await _prevQuery.FirstOrDefaultAsync();

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _prevQuery.FirstOrDefaultAsync(predicate);

        public async Task<T?> SingleOrDefaultAsync() => await _prevQuery.SingleOrDefaultAsync();

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _prevQuery.SingleOrDefaultAsync(predicate);

        public async Task<List<T>> ToListAsync() => await _prevQuery.ToListAsync();
    }
}
