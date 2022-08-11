using DALQueryChain.Interfaces;
using ManualTest.Linq2Db.Context;
using Microsoft.AspNetCore.Mvc;
using static ManualTest.Linq2Db.Context.IdentitySchema;

namespace Sample.Linq2Db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly IDALQueryChain<TestContext> _qs;

        public TestController(ILogger<TestController> logger, IDALQueryChain<TestContext> qs)
        {
            _logger = logger;
            _qs = qs;
        }

        [HttpGet(Name = "Test")]
        public IActionResult Get()
        {
            var user = _qs.For<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToList();

            return Ok();
        }
    }
}