using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DALQueryChain.EntityFramework.Builder
{
    internal class BaseQueryChain<TContext>
        where TContext : DbContext
    {
        protected readonly TContext _context;

        private readonly IDALQueryChain<TContext>? _defQC;

        private ConcurrentDictionary<Type, object?> _cachedRepositories = new();

        internal BaseQueryChain(TContext context, IDALQueryChain<TContext>? defQC = null)
        {
            _context = context;
            _defQC = defQC;
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
                obj = Activator.CreateInstance(repType, _context);
                _cachedRepositories.TryAdd(repType, obj);
            }

            var rep = (BaseRepository<TContext, TEntity>)obj!;
            rep.InitQueryChain(_defQC);

            return (TRepository?)(object)rep;
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

            var rep = (BaseRepository<TContext, TEntity>)obj!;
            rep.InitQueryChain(_defQC);

            return rep;

        }
    }
}
