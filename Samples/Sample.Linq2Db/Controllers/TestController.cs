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

        [HttpGet("OneTest")]
        public async Task<IActionResult> TestOne() 
        {
            var user = await _qs.For<User>().Insert.InsertWithObjectAsync(new User
            {
                AccessFailedCount = 1,
                CreateAt = DateTime.Now,
                DeleteAt = DateTime.Now,
                Email = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
                ModifyAt = DateTime.Now,
                PasswordHash = "",
                PhoneConfirmed = false,
                RoleId = 1,
                Salt = "",
                TwoFactorEnabled = false,
                Username = Guid.NewGuid().ToString()
            });

            var users1 = await _qs.For<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToListAsync();

            var users2 = await _qs.For<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToListAsync();

            return Ok();
        }
    }
}