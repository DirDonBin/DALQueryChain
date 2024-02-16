using DALQueryChain.Filter.Enums;
using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Builder;
using DALQueryChain.Tests.Linq2DB.Common.Fixtures.Objects;
using DALQueryChain.Tests.Linq2DB.Common.Models.Filters;
using DALQueryChain.Tests.Linq2DB.Common.TestCases;
using Linq2Db.DAL.Context;
using LinqToDB;
using LinqToDB.Data;
using ManualTest.Linq2Db.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Tests.Linq2DB.Tests
{
    [Collection("Linq2DbDatabase")]
    public class QueryTests : Linq2dbDatabaseTestCase
    {
        private readonly IDALQueryChain<TestContext> _dqc;

        public QueryTests(Linq2DbDatabaseFixture databaseFixture) : base(databaseFixture)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            _dqc = new BuildQuery<TestContext>(DbContext, serviceProvider.Object);
        }

        [Fact]
        public async Task FirstTest()
        {
            var expectFirst = DbContext.Products.First();
            var resultFirst = _dqc.For<Product>().Get.First();

            Assert.Equivalent(expectFirst, resultFirst);

            var expectFirstOrDefault = DbContext.Products.FirstOrDefault();
            var resultFirstOrDefault = _dqc.For<Product>().Get.FirstOrDefault();

            Assert.Equivalent(expectFirstOrDefault, resultFirstOrDefault);

            var expectFirstAsync = await DbContext.Products.FirstAsync();
            var resultFirstAsync = await _dqc.For<Product>().Get.FirstAsync();

            Assert.Equivalent(expectFirstAsync, resultFirstAsync);

            var expectFirstOrDefaultAsync = await DbContext.Products.FirstOrDefaultAsync();
            var resultFirstOrDefaultAsync = await _dqc.For<Product>().Get.FirstOrDefaultAsync();

            Assert.Equivalent(expectFirstOrDefaultAsync, resultFirstOrDefaultAsync);
        }

        [Fact]
        public async Task SingleTest()
        {
            var expectSingle = DbContext.Products.Single(x => x.Id == 5);
            var resultSingle = _dqc.For<Product>().Get.Single(x => x.Id == 5);

            Assert.Equivalent(expectSingle, resultSingle);

            var expectSingleOrDefault = DbContext.Products.SingleOrDefault(x => x.Id == 5);
            var resultSingleOrDefault = _dqc.For<Product>().Get.SingleOrDefault(x => x.Id == 5);

            Assert.Equivalent(expectSingleOrDefault, resultSingleOrDefault);

            var expectSingleAsync = await DbContext.Products.SingleAsync(x => x.Id == 5);
            var resultSingleAsync = await _dqc.For<Product>().Get.SingleAsync(x => x.Id == 5);

            Assert.Equivalent(expectSingleAsync, resultSingleAsync);

            var expectSingleOrDefaultAsync = await DbContext.Products.SingleOrDefaultAsync(x => x.Id == 5);
            var resultSingleOrDefaultAsync = await _dqc.For<Product>().Get.SingleOrDefaultAsync(x => x.Id == 5);

            Assert.Equivalent(expectSingleOrDefaultAsync, resultSingleOrDefaultAsync);


            Assert.Throws<InvalidOperationException>(() => _dqc.For<Product>().Get.SingleOrDefault());
        }

        [Fact]
        public async Task LastTest()
        {
            var expectLast = DbContext.Products.OrderByDescending(x => x).First();
            var resultLast = _dqc.For<Product>().Get.Last();

            Assert.Equivalent(expectLast, resultLast);

            var expectLastOrDefault = DbContext.Products.OrderByDescending(x => x).FirstOrDefault();
            var resultLastOrDefault = _dqc.For<Product>().Get.LastOrDefault();

            Assert.Equivalent(expectLastOrDefault, resultLastOrDefault);

            var expectLastAsync = await DbContext.Products.OrderByDescending(x => x).FirstAsync();
            var resultLastAsync = await _dqc.For<Product>().Get.LastAsync();

            Assert.Equivalent(expectLastAsync, resultLastAsync);

            var expectLastOrDefaultAsync = await DbContext.Products.OrderByDescending(x => x).FirstOrDefaultAsync();
            var resultLastOrDefaultAsync = await _dqc.For<Product>().Get.LastOrDefaultAsync();

            Assert.Equivalent(expectLastOrDefaultAsync, resultLastOrDefaultAsync);
        }

        [Fact]
        public async Task ToListTest()
        {
            var data = await DbContext.Products.OrderByDescending(x => x).ToListAsync();

            var query = await _dqc.For<Product>().Get.OrderByDescending(x => x.Name).Reverse().ToListAsync();
            var qq = await _dqc.For<Product>().Get.ToListAsync();
            var recsq = _dqc.For<Product>().Get.Reverse();
            var recs11q = _dqc.For<Product>().Get.Reverse().Reverse();
            var recs1q = _dqc.For<Product>().Get.Reverse().Where(x => true).Reverse();
            var recs2q = _dqc.For<Product>().Get.Reverse().Take(50).Reverse();

            var recs = await recsq.ToListAsync();
            var recs11 = await recs11q.ToListAsync();
            var recs1 = await recs1q.ToListAsync();
            var recs2 = await recs2q.ToListAsync();
        }
    }
}
