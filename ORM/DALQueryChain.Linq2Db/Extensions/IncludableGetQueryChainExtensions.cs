using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Linq2Db.Extensions
{
    public static class IncludableGetQueryChainExtensions
    {
        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, TPreviousProperty> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((ILoadWithQueryable<TEntity, TPreviousProperty>)source.Query).ThenLoad(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, IEnumerable<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((ILoadWithQueryable<TEntity, IEnumerable<TPreviousProperty>>)source.Query).ThenLoad(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, IList<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((ILoadWithQueryable<TEntity, IList<TPreviousProperty>>)source.Query).ThenLoad(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, List<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> selector
            )
            where TEntity : class
        {
            var qr = ((ILoadWithQueryable<TEntity, List<TPreviousProperty>>)source.Query).ThenLoad(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }
    }
}
