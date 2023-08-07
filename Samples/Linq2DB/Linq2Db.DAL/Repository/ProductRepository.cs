using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Extensions;
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

    public class TTest
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }

    public static class Huita
    {
        public static TTest ToModel(Product obj)
        {
            return new TTest()
            {
                Id = obj.Id,
                Id2 = obj.Category == null ? 0 : obj.Category.Id,
                Name = obj.Name,
                CategoryName = obj.Category == null ? "" : obj.Category.Name,
                //Count = obj.Category.Products.Count()
            };
        }

        public static TTest ToModel2(Category obj)
        {
            return new TTest()
            {
                Id = obj.Id,
                Name = obj.Name,
                Count = obj.Products.Count()
            };
        }
    }

    public class ProductRepository : BaseTestContextRepository<Product>
    {
        public ProductRepository(TestContext context) : base(context)
        {
        }

        public async Task Test()
        {
            var ere = _context.Categories.LoadWith(x => x.Products, x => x.Where(y => y.Name.Contains("Fresh"))).AsQueryChain();
            var ttt = await _context.Categories.LoadWith(x => x.Products, x => x.Where(y => y.Name.Contains("Fresh"))).ToListAsync();
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
