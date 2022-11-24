using Bogus;
using DALQueryChain.Filter;
using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Extensions;
using DALQueryChain.Filter.Models;
using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Extensions;
using GrEmit;
using ManualTest.Linq2Db.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using static LinqToDB.Common.Configuration;
using static ManualTest.Linq2Db.Context.TestContext;

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


        [HttpPost("[action]")]
        public async Task<IActionResult> TestIlFilter(TestFilterModel filter)
        {

            //var test1 = await _qs.For<Product>().Get.Filter(filter).ToListAsync(); //Work
            var test2 = await _qs.For<Product>().Get.Filter(filter, Filters.IdEquals).ToListAsync();
            var test3 = await _qs.For<Product>().Get.Filter(filter, Filters.IdGreater).ToListAsync();
            var test4 = await _qs.For<Product>().Get.Filter(filter, Filters.ContainsName).ToListAsync();
            var test5 = await _qs.For<Product>().Get.Filter(filter, Filters.BetweenDate).ToListAsync();
            var test6 = await _qs.For<Product>().Get.Filter(filter, Filters.Category).ToListAsync();
            var test7 = await _qs.For<Product>().Get.Filter(filter, Filters.Categories).ToListAsync();
            var test8 = await _qs.For<Product>().Get.Filter(filter, Filters.Ids).ToListAsync();
            var test9 = await _qs.For<Product>().Get.Filter(filter, Filters.Count).ToListAsync();
            var test10 = await _qs.For<Product>().Get.Filter(filter, Filters.Raiting1).ToListAsync();
            var test11 = await _qs.For<Product>().Get.Filter(filter, Filters.Raiting2).ToListAsync();
            var test12 = await _qs.For<Product>().Get.Filter(filter, Filters.PriceGreater).ToListAsync();
            var test13 = await _qs.For<Product>().Get.Filter(filter, Filters.PriceLess).ToListAsync();

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult TestTestTest()
        {
            var method = new DynamicMethod("FilterFuncImpl", // имя метода
                                  typeof(bool),
                                  new[] { typeof(int), typeof(int?) },
                                  true);

            var il = new GroboIL(method);

            il.Nop();

            //var get1 = typeof(int).GetMethod($"get_{propertyName}");

            il.Nop();
            il.Ret();

            return Ok();
        }

    }

    public enum Filters
    {
        IdEquals,
        IdGreater,
        ContainsName,
        BetweenDate,
        Category,
        Categories,
        Ids,
        Count,
        Raiting1,
        Raiting2,
        PriceGreater,
        PriceLess
    }

    [QCFilter(Filters.IdEquals)]
    [QCFilter(Filters.ContainsName)]
    [QCFilter(Filters.BetweenDate)]
    [QCFilter(Filters.Category)]
    [QCFilter(Filters.Categories)]
    [QCFilter(Filters.Ids)]
    [QCFilter(Filters.Count)]
    [QCFilter(Filters.Raiting1)]
    [QCFilter(Filters.Raiting2)]
    [QCFilter(Filters.PriceGreater)]
    [QCFilter(Filters.PriceLess)]
    public class TestFilterModel
    {
        [QCFilterField(Filters.IdEquals, QSFilterConditionType.Equals)]
        [QCFilterField(Filters.IdGreater, QSFilterConditionType.Greater)]
        public int? Id { get; set; }

        [QCFilterField(Filters.ContainsName, QSFilterConditionType.Contains)]
        public string? Name { get; set; }

        [QCFilterField(Filters.BetweenDate, QSFilterConditionType.GreaterOrEqual, FieldName = nameof(Product.Created))]
        public DateTime? DateFrom { get; set; }

        [QCFilterField(Filters.BetweenDate, QSFilterConditionType.Less, FieldName = nameof(Product.Created))]
        public DateTime? DateTo { get; set; }

        [QCFilterField(Filters.Category, QSFilterConditionType.Equals)]
        public int? CategoryId { get; set; }

        [QCFilterField(Filters.Categories, QSFilterConditionType.Contains, FieldName = nameof(Product.CategoryId))]
        public List<int>? Categories { get; set; }

        [QCFilterField(Filters.Ids, QSFilterConditionType.Contains, FieldName = nameof(Product.Id))]
        public int[]? Ids { get; set; }

        [QCFilterField(Filters.Ids, QSFilterConditionType.GreaterOrEqual)]
        public int? Count { get; set; }

        [QCFilterField(Filters.Raiting1, QSFilterConditionType.Greater, FieldName = nameof(Product.Raiting))]
        public int? Raiting1 { get; set; }

        [QCFilterField(Filters.Raiting2, QSFilterConditionType.LessOrEqual, FieldName = nameof(Product.Raiting))]
        public float? Raiting2 { get; set; }

        [QCFilterField(Filters.PriceGreater, QSFilterConditionType.Greater)]
        public decimal? Price { get; set; }
    }
}