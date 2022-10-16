using DALQueryChain.Interfaces;
using LinqToDB.Data;

namespace DALQueryChain.Linq2Db.Builder
{
    internal class BaseQueryChain<TContext>
        where TContext : DataConnection
    {
        protected readonly TContext _context;

        protected readonly IDALQueryChain<TContext> _defQC;

        internal BaseQueryChain(TContext context, IDALQueryChain<TContext> defQC)
        {
            _context = context;
            _defQC = defQC;
        }
    }
}
