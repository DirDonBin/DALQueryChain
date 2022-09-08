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
            where TEntity : class
        {
            var qr = ((IIncludableQueryable<TEntity, TPreviousProperty>)source.Query).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, IEnumerable<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>>)source.Query).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, IList<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((IIncludableQueryable<TEntity, IList<TPreviousProperty>>)source.Query).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, List<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((IIncludableQueryable<TEntity, List<TPreviousProperty>>)source.Query).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }
    }
}
