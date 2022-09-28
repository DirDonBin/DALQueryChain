using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB.Data;
using Sample.Linq2Db.Controllers;

namespace Sample.Linq2Db.Repositories
{
    public abstract class TryBaseRepo<TContext, TEntity>: BaseRepository<TContext, TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        protected readonly ITestDI _testDI;

        protected TryBaseRepo(TContext context, ITestDI testDI) : base(context)
        {
            _testDI = testDI;
        }
    }
}
