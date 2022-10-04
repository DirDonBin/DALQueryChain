using DALQueryChain.Interfaces;
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

            await _qs.For<User>().Update.BulkUpdateAsync(users);

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