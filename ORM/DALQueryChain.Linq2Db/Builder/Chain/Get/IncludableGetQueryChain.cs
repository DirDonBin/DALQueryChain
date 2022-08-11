using DALQueryChain.Interfaces;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Linq2Db.Builder.Chain.Get
{
    internal class IncludableGetQueryChain<T, TPreviousProperty> : FilterableQueryChain<T>, IIncludableGetQueryChain<T, TPreviousProperty>
        where T : class
    {
        public IncludableGetQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IIncludableGetQueryChain<T, IEnumerable<TProperty>> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> selector)
        {
            return new IncludableGetQueryChain<T, IEnumerable<TProperty>>(((ILoadWithQueryable<T, TPreviousProperty>)_prevQuery).ThenLoad(selector));
        }

        public IIncludableGetQueryChain<T, TProperty> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, TProperty>> selector)
        {
            return new IncludableGetQueryChain<T, TProperty>(((ILoadWithQueryable<T, TPreviousProperty>)_prevQuery).ThenLoad(selector));
        }
    }
}
