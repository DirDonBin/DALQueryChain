using DALQueryChain.Interfaces;
using Sample.Linq2Db.Context;
using Microsoft.AspNetCore.Mvc;
using static Sample.Linq2Db.Context.IdentitySchema;

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
            var user = _qs.For<User>().Insert.InsertWithObject(new User
            {
                AccessFailedCount = 0,
                UserName = "test1",
                CreateAt = DateTime.Now,
                DeleteAt = DateTime.Now,
                EmailConfirmed = false,
                Id = Guid.NewGuid(),
                ModifyAt = DateTime.Now,
                PasswordHash = "",
                PhoneConfirmed = false,
                TwoFactorEnabled = false,
            });

            var users = _qs.For<User>().Get.ToList();

            return Ok();
        }
    }
}