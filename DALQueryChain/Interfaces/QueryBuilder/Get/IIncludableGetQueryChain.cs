using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Interfaces.QueryBuilder.Get
{
    public interface IIncludableGetQueryChain<T, TPreviousProperty>
        where T : notnull
        where TPreviousProperty : notnull
    {
        public IIncludableGetQueryChain<T, IEnumerable<TProperty>> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, IEnumerable<TProperty>>> selector) where TProperty : class;
        //public IIncludableGetQueryChain<T, TProperty> ThenLoad<TProperty>(Expression<Func<TPreviousProperty, TProperty>> selector) where TProperty : class;
    }
}
