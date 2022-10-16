using DALQueryChain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DALQueryChain.EntityFramework.Builder
{
    internal class BaseQueryChain<TContext>
        where TContext : DbContext
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
