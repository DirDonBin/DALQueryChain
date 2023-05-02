using DALQueryChain.EntityFramework.Builder.Chain;
using DALQueryChain.EntityFramework.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DALQueryChain.EntityFramework.Extensions
{
    public static class IncludableGetQueryChainExtensions
    {
        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, TPreviousProperty> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TPreviousProperty : class
            where TEntity : class
            where TProperty : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
            var qr = ((IIncludableQueryable<TEntity, TPreviousProperty>)prevQuery).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, IEnumerable<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TPreviousProperty : class
            where TEntity : class
            where TProperty : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
            var qr = ((IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>>)prevQuery).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> LoadWith<TEntity, TProperty>(this IFilterableQueryChain<TEntity> source,
            Expression<Func<TEntity, TProperty>> selector)
            where TProperty : class
            where TEntity : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
            return new IncludableGetQueryChain<TEntity, TProperty>(prevQuery.Include(selector));
        }
    }
}
