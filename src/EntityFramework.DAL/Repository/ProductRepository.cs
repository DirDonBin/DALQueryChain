using DALQueryChain.EntityFramework.Repositories;
using DALQueryChain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DAL.Repository
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
            //var t1 = _context.Products.SkipWhile(x => x.Id == 1).Take(10).AsSingleQuery;

            //var res = await t1.ToListAsync();
        }

        //protected override Task OnBeforeInsert(CancellationToken ctn = default)
        //{
        //    var t1 = _context.Transaction;
        //    return base.OnBeforeInsert(ctn);
        //}

        //protected override Task OnAfterInsert(CancellationToken ctn = default)
        //{
        //    var t1 = _context.Transaction;
        //    return base.OnAfterInsert(ctn);
        //}
    }
}
