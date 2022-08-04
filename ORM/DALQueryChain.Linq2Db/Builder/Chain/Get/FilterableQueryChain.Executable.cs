using DALQueryChain.Linq2Db.Builder.Chain.Get;
using DALQueryChain.Interfaces.QueryBuilder.Get;
using System.Linq.Expressions;

namespace DALQueryChain.Linq2Db.Builder.Chain
{
    internal partial class FilterableQueryChain<T> : BaseGetQueryChain<T>, IFilterableQueryChain<T>
        where T : class
    {
        
    }
}
