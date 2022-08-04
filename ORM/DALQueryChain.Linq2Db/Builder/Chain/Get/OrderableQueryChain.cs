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
    internal class OrderableQueryChain<T> : FilterableQueryChain<T>, IOrderableQueryChain<T>
        where T : class
    {
        public OrderableQueryChain(IQueryable<T> prevQuery) : base(prevQuery)
        {
        }

        public IOrderableQueryChain<T> ThenBy(Expression<Func<T, object>> selector)
        {
            return new OrderableQueryChain<T>(((IOrderedQueryable<T>)_prevQuery).ThenBy(selector));
        }

        public IOrderableQueryChain<T> ThenByDescending(Expression<Func<T, object>> selector)
        {
            return new OrderableQueryChain<T>(((IOrderedQueryable<T>)_prevQuery).ThenByDescending(selector));
        }
    }
}
