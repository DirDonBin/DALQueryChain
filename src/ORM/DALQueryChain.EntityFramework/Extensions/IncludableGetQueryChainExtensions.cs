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
            var qr = ((IIncludableQueryable<TEntity, TPreviousProperty>)source.Query).ThenInclude(selector);
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
            var qr = ((IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>>)source.Query).ThenInclude(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> LoadWith<TEntity, TProperty>(this IFilterableQueryChain<TEntity> source,
            Expression<Func<TEntity, TProperty>> selector)
            where TProperty : class
            where TEntity : class => new IncludableGetQueryChain<TEntity, TProperty>(source.Query.Include(selector));
    }
}
