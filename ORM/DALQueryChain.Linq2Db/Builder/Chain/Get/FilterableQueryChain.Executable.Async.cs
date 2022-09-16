using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
        where T : class
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

        public async Task<List<T>> ToListAsync(CancellationToken ctn = default) => await _prevQuery.ToListAsync(ctn);
    }
}
