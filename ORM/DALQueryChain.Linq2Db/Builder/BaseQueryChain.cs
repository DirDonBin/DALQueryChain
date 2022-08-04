using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB.Data;
using System.Collections.Concurrent;

namespace DALQueryChain.Linq2Db.Builder
{
    internal class BaseQueryChain<TContext>
        where TContext : DataConnection
    {
        protected readonly TContext _context;
        private static ConcurrentDictionary<Type, object?> _cachedRepositories = new();

        internal BaseQueryChain(TContext context)
        {
            _context = context;
        }

        internal TRepository? GetRepository<TRepository, TEntity>()
            where TRepository : IRepository
            where TEntity : class, IDbModelBase
        {
            var repType = Configure.CachedRepoTypes.FirstOrDefault(x => typeof(BaseRepository<TContext, TEntity>).IsAssignableFrom(x));

            if (repType is null)
                throw new NullReferenceException($"Repository {nameof(TRepository)} inherited from BaseRepository<{nameof(TContext)}, {nameof(TEntity)}> not found");

            object? obj = default;

            if (!_cachedRepositories.TryGetValue(repType, out obj) || obj is null)
            {
                obj = Activator.CreateInstance(repType);
                _cachedRepositories.TryAdd(repType, obj);
            }

            return (TRepository?)obj;
        }

        internal BaseRepository<TContext, TEntity> GetGenericRepository<TEntity>()
            where TEntity : class, IDbModelBase
        {
            object? obj = null;

            var repType = Configure.CachedRepoTypes.FirstOrDefault(x => typeof(BaseRepository<TContext, TEntity>).IsAssignableFrom(x));

            if (repType is not null && (!_cachedRepositories.TryGetValue(repType, out obj) || obj is null))
            {
                obj = Activator.CreateInstance(repType, _context);
                _cachedRepositories.TryAdd(repType, obj);
            }

            if (obj is null)
            {
                repType = typeof(BaseRepository<TContext, TEntity>);
                if (!_cachedRepositories.TryGetValue(repType, out obj) || obj is null)
                {
                    obj = new GenericRepository<TContext, TEntity>(_context);
                    _cachedRepositories.TryAdd(repType, (BaseRepository<TContext, TEntity>)obj);
                }
            }

            return (BaseRepository<TContext, TEntity>)obj!;

        }
    }
}
