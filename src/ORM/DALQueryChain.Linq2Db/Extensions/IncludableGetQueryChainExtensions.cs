﻿using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Builder;
using DALQueryChain.Linq2Db.Builder.Chain;
using DALQueryChain.Linq2Db.Builder.Chain.Get;
using LinqToDB;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Extensions
{
    public static class IncludableGetQueryChainExtensions
    {
        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, TPreviousProperty> source,
            Expression<Func<TPreviousProperty, TProperty?>> selector
            )
            where TPreviousProperty : class
            where TProperty : class
            where TEntity : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
            var qr = ((ILoadWithQueryable<TEntity, TPreviousProperty>)prevQuery).ThenLoad(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
            this IIncludableGetQueryChain<TEntity, IEnumerable<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty?>> selector
            )
            where TProperty : class
            where TEntity : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
            var qr = ((ILoadWithQueryable<TEntity, IEnumerable<TPreviousProperty>>)prevQuery).ThenLoad(selector);
            return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        }

        //public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
        //    this IIncludableGetQueryChain<TEntity, TPreviousProperty> source,
        //    Expression<Func<TPreviousProperty, TProperty?>> selector,
        //    Expression<Func<IFilterableQueryChain<TProperty>, IFilterableQueryChain<TProperty>>> loadFunc
        //    )
        //    where TPreviousProperty : class
        //    where TProperty : class
        //    where TEntity : class
        //{
        //    var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
        //    var loadQuery = (ILoadWithQueryable<TEntity, TPreviousProperty>)prevQuery;
        //    var func = ((FilterableQueryChain<TProperty>)loadFunc.Compile()(loadQuery.Select(selector!)!)).Query;
        //    var qr = loadQuery.ThenLoad(selector);
        //    return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        //}

        //public static IIncludableGetQueryChain<TEntity, TProperty> ThenLoad<TEntity, TPreviousProperty, TProperty>(
        //    this IIncludableGetQueryChain<TEntity, IEnumerable<TPreviousProperty>> source,
        //    Expression<Func<TPreviousProperty, TProperty?>> selector,
        //    Expression<Func<IFilterableQueryChain<TProperty>, IFilterableQueryChain<TProperty>>> loadFunc
        //    )
        //    where TProperty : class
        //    where TEntity : class
        //{
        //    var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
        //    var qr = ((ILoadWithQueryable<TEntity, IEnumerable<TPreviousProperty>>)prevQuery).ThenLoad(selector);
        //    return new IncludableGetQueryChain<TEntity, TProperty>(qr);
        //}



        public static IIncludableGetQueryChain<TEntity, TProperty> LoadWith<TEntity, TProperty>(this IFilterableQueryChain<TEntity> source,
            Expression<Func<TEntity, TProperty?>> selector)
            where TProperty : class
            where TEntity : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;
            return new IncludableGetQueryChain<TEntity, TProperty>(prevQuery.LoadWith(selector));
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> LoadWith<TEntity, TProperty>(
            this IFilterableQueryChain<TEntity> source,
            Expression<Func<TEntity, IEnumerable<TProperty>?>> selector,
            Expression<Func<IFilterableQueryChain<TProperty>, IFilterableQueryChain<TProperty>>> loadFunc
            )
            where TProperty : class
            where TEntity : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;

            var func = ((FilterableQueryChain<TProperty>)loadFunc.Compile()(source.SelectMany(selector!)!)).Query;

            return new IncludableGetQueryChain<TEntity, TProperty>(prevQuery.LoadWith(selector, x => func));
        }

        public static IIncludableGetQueryChain<TEntity, TProperty> LoadWith<TEntity, TProperty>(
            this IFilterableQueryChain<TEntity> source,
            Expression<Func<TEntity, TProperty?>> selector,
            Expression<Func<IFilterableQueryChain<TProperty>, IFilterableQueryChain<TProperty>>> loadFunc
            )
            where TProperty : class
            where TEntity : class
        {
            var prevQuery = ((FilterableQueryChain<TEntity>)source).Query;

            var func = ((FilterableQueryChain<TProperty>)loadFunc.Compile()(source.Select(selector)!)).Query;

            return new IncludableGetQueryChain<TEntity, TProperty>(prevQuery.LoadWith(selector, x => func));
        }


    }
}
