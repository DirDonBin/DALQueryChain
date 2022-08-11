using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IIncludableGetQueryChain<T, TPreviousProperty> : IFilterableQueryChain<T>
        where T : class
    {
        public IIncludableGetQueryChain<T, IEnumerable<TProperty>> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> selector);
        public IIncludableGetQueryChain<T, TProperty> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, TProperty>> selector);
    }
}
