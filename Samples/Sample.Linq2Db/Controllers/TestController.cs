using DALQueryChain.Filter;
using DALQueryChain.Filter.Models;
using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Extensions;
using ManualTest.Linq2Db.Context;
using Microsoft.AspNetCore.Mvc;
using Sample.Linq2Db.Repositories;
using static ManualTest.Linq2Db.Context.IdentitySchema;

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

        [HttpGet("OneTest")]
        public async Task<IActionResult> TestOne()
        {
            var users = await _qs.For<User>().Get.ToListAsync();

            foreach (var item in users)
            {
                item.Email = $"Email iteration of {users.IndexOf(item)}";
            }

            await _qs.For<User>().Get.LoadWith(x => x.UsersSettingsIdId).ThenLoad(x => x.FkUsersSettingsIdUsersId).ThenLoad(x => x.UsersSettingsIdId).ToListAsync();

            var tmp = 0;

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult TestFilter()
        {
            var list = new List<TestModel>() {
                new TestModel { Id = 1, Name = "Test 1", DateCreate = DateTime.Now.AddDays(-7) },
                new TestModel { Id = 2, Name = "Test 2", DateCreate = DateTime.Now.AddDays(-6) },
                new TestModel { Id = 3, Name = "Test 3", DateCreate = DateTime.Now.AddDays(-5) },
                new TestModel { Id = 4, Name = "Test 4", DateCreate = DateTime.Now.AddDays(-4) },
                new TestModel { Id = 5, Name = "Test 5", DateCreate = DateTime.Now.AddDays(-3) },
                new TestModel { Id = 6, Name = "Test 6", DateCreate = DateTime.Now.AddDays(-2) },
                new TestModel { Id = 7, Name = "Test 7", DateCreate = DateTime.Now.AddDays(-1) },
                new TestModel { Id = 8, Name = "Test 8", DateCreate = DateTime.Now },
                new TestModel { Id = 9, Name = "Test 9", DateCreate = DateTime.Now.AddDays(1) },
                new TestModel { Id = 10, Name = "Test 10", DateCreate = DateTime.Now.AddDays(2) }
            };

            var testModel = new TestModel()
            {
                Id = 5,
                Name = "3",
                DateCreate = DateTime.Now.AddDays(-2)
            };

            var result = TestMethods.FilterTestModels(list, testModel);

            return Ok();
        }

    }

    public interface ITestDI
    {
        void Test();
    }

    public class TestDI : ITestDI
    {
        public void Test() { }
    }
}