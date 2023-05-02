using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using LinqToDB.Data;
using ManualTest.Linq2Db.Context;

namespace Linq2Db.DAL.Repository
{
    public class BaseTestContextRepository<TEntity> : BaseRepository<TestContext, TEntity>
        where TEntity : class, IDbModelBase
    {
        public BaseTestContextRepository(TestContext context) : base(context)
        {
        }
    }

    public class ProductRepository : BaseTestContextRepository<Product>
    {
        public ProductRepository(TestContext context) : base(context)
        {
        }

        public async Task Test()
        {
            var ttt = new List<Product>();
            await _context.BulkCopyAsync(ttt);
        }

        protected override Task OnBeforeInsert(CancellationToken ctn = default)
        {
            return base.OnBeforeInsert(ctn);
        }

        protected override Task OnAfterInsert(CancellationToken ctn = default)
        {
            return base.OnAfterInsert(ctn);
        }
    }
}
