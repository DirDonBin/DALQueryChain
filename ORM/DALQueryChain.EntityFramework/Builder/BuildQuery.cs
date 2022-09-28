using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Extensions;
using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;

namespace DALQueryChain.EntityFramework.Builder
{
    /// <summary>
    /// Helper for building queries to database
    /// </summary>
    /// <typeparam name="TContext">Data Connection</typeparam>
    public class BuildQuery<TContext> : IDALQueryChain<TContext>
        where TContext : notnull, DbContext
    {
        private readonly TContext _context;
        private readonly ConcurrentDictionary<Type, object> _cacheQBC;
        private readonly ConcurrentDictionary<Type, object?> _cachedRepositories = new();
        private readonly IServiceProvider _serviceProvider;

        public BuildQuery(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _cacheQBC = new();
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Create query for specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        public IQueryBuilder<TEntity> For<TEntity>() where TEntity : class, IDbModelBase
        {
            var qbc = _cacheQBC.ContainsKey(typeof(TEntity))
                ? _cacheQBC[typeof(TEntity)]
                : _cacheQBC.GetOrAdd(typeof(TEntity), new QueryBuilderChain<TContext, TEntity>(_context, this));

            return (IQueryBuilder<TEntity>)qbc;
        }

        public void Transaction(Action<IDALQueryChain<TContext>> operation)
        {
            using var tr = _context.Database.BeginTransaction();

            try
            {
                operation.Invoke(this);

                tr.Commit();
            }
            catch (Exception)
            {
                tr.Rollback();
                throw;
            }
        }

        public async Task TransactionAsync(Func<IDALQueryChain<TContext>, CancellationToken, Task> operation, CancellationToken ctn = default)
        {
            using var tr = await _context.Database.BeginTransactionAsync(ctn);

            try
            {
                await operation.Invoke(this, ctn);

                await tr.CommitAsync(ctn);
            }
            catch (Exception)
            {
                await tr.RollbackAsync(ctn);
                throw;
            }
        }

        public TRepository Repository<TRepository>()
        {
            var repType = Configure.CachedRepoTypes
                .Where(x => x.IsAssignableToGenericType(typeof(BaseRepository<,>)))
                .FirstOrDefault(x => x.BaseType is not null && x.BaseType.GenericTypeArguments.Any(y => y == typeof(TContext)) && x.Name == typeof(TRepository).Name);

            if (repType is null)
                throw new NullReferenceException($"Repository {nameof(TRepository)} inherited from BaseRepository<,> not found");


            if (!_cachedRepositories.TryGetValue(repType, out object? obj) || obj is null)
            {
                obj = ActivatorUtilities.CreateInstance(_serviceProvider, repType, _context);
                _cachedRepositories.TryAdd(repType, obj);
            }

            var entityType = repType.BaseType!.GenericTypeArguments.First(x => typeof(IDbModelBase).IsAssignableFrom(x));

            var methodInfo = typeof(TRepository).BaseType!.GetMethodByAllParrent("InitQueryChain", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            methodInfo!.Invoke(obj, new object[] { this });

            return (TRepository)obj!;
        }

        internal BaseRepository<TContext, TEntity> GetGenericRepository<TEntity>()
            where TEntity : class, IDbModelBase
        {
            object? obj = null;

            var repType = Configure.CachedRepoTypes.FirstOrDefault(x => typeof(BaseRepository<TContext, TEntity>).IsAssignableFrom(x));

            if (repType is not null && (!_cachedRepositories.TryGetValue(repType, out obj) || obj is null))
            {
                obj = ActivatorUtilities.CreateInstance(_serviceProvider, repType, _context);
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
            rep.InitQueryChain(this);

            return rep;

        }
    }
}
