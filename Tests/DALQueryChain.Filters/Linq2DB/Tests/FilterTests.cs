using DALQueryChain.Filter.Extensions;
using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Builder;
using DALQueryChain.Tests.Linq2DB.Common.Fixtures.Objects;
using DALQueryChain.Tests.Linq2DB.Common.Models.Filters;
using DALQueryChain.Tests.Linq2DB.Common.TestCases;
using ManualTest.Linq2Db.Context;
using Moq;
using Npgsql;

namespace DALQueryChain.Tests.Linq2DB.Tests
{
    [Collection("Linq2DbDatabase")]
    public class FilterTests : Linq2dbDatabaseTestCase
    {
        private readonly IDALQueryChain<TestContext> _dqc;
        private readonly TestFilterModel _filter;
        private readonly TestFilterModel _nullableFilter;

        public FilterTests(Linq2DbDatabaseFixture databaseFixture) : base(databaseFixture)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            _dqc = new BuildQuery<TestContext>(DbContext, serviceProvider.Object);

            _nullableFilter = new TestFilterModel();

            _filter = new TestFilterModel()
            {
                Id = Random.Shared.Next(1, 10000),
                Name = "Fresh",
                Price = Random.Shared.Next(20000, 50000),
                Count = Random.Shared.Next(0, 20),
                DateFrom = new DateTime(2021, 01, 01),
                DateTo = new DateTime(2022, 01, 01),
                Raiting1 = 3,
                Raiting2 = 2.25f,
                Ids = new[] { 5, 22, 365, 844, 1236, 5487 }
            };
        }

        [Fact]
        public void NullableFilterTest()
        {
            TestFilterModel? filter = null;

            var expect = DbContext.Products.Where(x => filter != null).ToList();
            var result = _dqc.For<Product>().Get.Filter(filter).ToList(); // add exception

            Assert.Equivalent(expect, result);
        }

        [Fact]
        public void AllObjectTest()
        {
            var expect = DbContext.Products.ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter).ToList();

            Assert.Equivalent(expect, result);
        }

        [Fact]
        public void EqualsTest()
        {
            var expect = DbContext.Products.FirstOrDefault(x => x.Id == _filter.Id);
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.IdEquals).FirstOrDefault();

            Assert.Equivalent(expect, result);

            var expectNullable = DbContext.Products.FirstOrDefault(x => x.Id == _nullableFilter.Id);
            var resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.IdEquals).FirstOrDefault();

            Assert.Equivalent(expectNullable, resultNullable);
        }

        [Fact]
        public void ContainsStringTest()
        {
            var expect = DbContext.Products.Where(x => x.Name.Contains(_filter.Name)).ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.ContainsName).ToList();

            Assert.Equivalent(expect, result);

            var expectNullable = () => DbContext.Products.Where(x => x.Name.Contains(_nullableFilter.Name)).ToList();
            var resultNullable = () => _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.ContainsName).ToList();

            var excpectThrow = Assert.Throws<PostgresException>(expectNullable);
            var resultThrow = Assert.Throws<PostgresException>(expectNullable);

            Assert.Equal(excpectThrow.Message, resultThrow.Message);

            var filter = new TestFilterModel()
            {
                Name = "111222333!@#$%"
            };

            var expectNotContains = DbContext.Products.Where(x => x.Name.Contains(filter.Name)).ToList();
            var resultNotContains = _dqc.For<Product>().Get.Filter(filter, TestFiltersEnum.ContainsName).ToList();

            Assert.Equivalent(expectNotContains, resultNotContains);
        }

        [Fact]
        public void GreaterTest()
        {
            var expect = DbContext.Products.Where(x => _filter.Id > x.Id).ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.IdGreater).ToList();

            Assert.Equivalent(expect, result);

            expect = DbContext.Products.Where(x => _filter.Price > x.Price).ToList();
            result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.PriceGreater).ToList();

            Assert.Equivalent(expect, result);

            var expectNullable = DbContext.Products.Where(x => _nullableFilter.Id > x.Id).ToList();
            var resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.IdGreater).ToList();

            Assert.Equivalent(expectNullable, resultNullable);

            expectNullable = DbContext.Products.Where(x => _nullableFilter.Price > x.Price).ToList();
            resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.PriceGreater).ToList();

            Assert.Equivalent(expectNullable, resultNullable);
        }

        [Fact]
        public void LessTest()
        {
            var expect = DbContext.Products.Where(x => _filter.Price < x.Price).ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.PriceLess).ToList();

            Assert.Equivalent(expect, result);

            var expectNullable = DbContext.Products.Where(x => _nullableFilter.Price > x.Price).ToList();
            var resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.PriceLess).ToList();

            Assert.Equivalent(expectNullable, resultNullable);
        }

        [Fact]
        public void BetweenTest()
        {
            var expect = DbContext.Products.Where(x => _filter.DateFrom <= x.Created && _filter.DateTo > x.Created).ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.BetweenDate).ToList();

            Assert.Equivalent(expect, result);

            var expectNullable = DbContext.Products.Where(x => _nullableFilter.DateFrom <= x.Created && _nullableFilter.DateTo > x.Created).ToList();
            var resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.BetweenDate).ToList();

            Assert.Equivalent(expectNullable, resultNullable);
        }

        [Fact]
        public void OrEqualsTest()
        {
            var expect = DbContext.Products.Where(x => _filter.Raiting2 <= x.Raiting).ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.Raiting2).ToList();

            Assert.Equivalent(expect, result);

            expect = DbContext.Products.Where(x => _filter.Count >= x.Count).ToList();
            result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.Count).ToList();

            Assert.Equivalent(expect, result);

            var expectNullable = DbContext.Products.Where(x => _nullableFilter.Raiting2 <= x.Raiting).ToList();
            var resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.Raiting2).ToList();

            Assert.Equivalent(expectNullable, resultNullable);

            expectNullable = DbContext.Products.Where(x => _nullableFilter.Count >= x.Count).ToList();
            resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.Count).ToList();

            Assert.Equivalent(expectNullable, resultNullable);
        }

        [Fact]
        public void EnumerableContainsTest()
        {
            var expect = DbContext.Products.Where(x => _filter.Ids.Contains(x.Id)).ToList();
            var result = _dqc.For<Product>().Get.Filter(_filter, TestFiltersEnum.Ids).ToList();

            Assert.Equivalent(expect, result);

            var expectNullable = DbContext.Products.Where(x => _nullableFilter.Ids.Contains(x.Id)).ToList();
            var resultNullable = _dqc.For<Product>().Get.Filter(_nullableFilter, TestFiltersEnum.Ids).ToList();

            Assert.Equivalent(expectNullable, resultNullable);
        }

    }
}
