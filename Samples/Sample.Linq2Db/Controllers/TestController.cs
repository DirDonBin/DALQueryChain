using Bogus;
using DALQueryChain.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Extensions;
using Linq2Db.DAL.Repository;
using LinqToDB;
using ManualTest.Linq2Db.Context;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Linq2Db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDALQueryChain<TestContext> _qs;

        public TestController(IDALQueryChain<TestContext> qs)
        {
            _qs = qs;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GenerateDatabaseData()
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(o => o.Name, (f, u) => f.Commerce.Categories(1)[0])
                .RuleFor(o => o.Created, (f, u) => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now));

            var categories = categoryFaker.Generate(10);

            await _qs.For<Category>().Insert.BulkInsertAsync(categories);
            categories = await _qs.For<Category>().Get.ToListAsync();

            var productFaker = new Faker<Product>()
                .RuleFor(o => o.Name, (f, u) => new string(f.Commerce.ProductName().Take(120).ToArray()))
                .RuleFor(o => o.Created, (f, u) => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
                .RuleFor(o => o.Price, (f, u) => decimal.Parse(f.Commerce.Price(1000, 100000)))
                .RuleFor(o => o.Count, (f, u) => f.Random.Int(0, 20).OrNull(f))
                .RuleFor(o => o.CategoryId, (f, u) => f.Random.ListItem(categories).OrNull(f)?.Id)
                .RuleFor(o => o.Raiting, (f, u) => f.Random.Double(1, 5).OrNull(f));

            var products = productFaker.Generate(5000);

            try
            {
                await _qs.For<Product>().Insert.BulkInsertAsync(products);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TestQWER()
        {

            //var ttt = await _qs.For<Product>().Get.LoadWith(x => x.Category).Where(x => x.Category == null).ToListAsync();

            var trt = _qs.For<Category>().Get.LoadWith(x => x.Products, x => x.Where(y => y.Name.Contains("Fresh"))).Sum(x => x.Id).ToString();

            await _qs.Repository<ProductRepository>().Test();

            var ttt = await _qs.For<Category>().Get.LoadWith(x => x.Products, x => x.Where(y => y.Name.Contains("Fresh"))).ToListAsync();

            return Ok();
        }
    }
}