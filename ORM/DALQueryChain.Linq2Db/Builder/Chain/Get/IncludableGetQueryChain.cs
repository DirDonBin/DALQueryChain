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
        where TPreviousProperty : notnull
    {
        public IncludableGetQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IIncludableGetQueryChain<T, IEnumerable<TProperty>> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> selector) where TProperty : class
        {
            return new IncludableGetQueryChain<T, IEnumerable<TProperty>>(((ILoadWithQueryable<T, TPreviousProperty>)_prevQuery).ThenLoad(selector));
        }
    }
}
