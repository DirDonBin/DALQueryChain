using DALQueryChain.Interfaces;
using DALQueryChain.Linq2Db.Extensions;
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
            //var user = await _qs.For<User>().Insert.InsertWithObjectAsync(new User
            //{
            //    AccessFailedCount = 1,
            //    CreateAt = DateTime.Now,
            //    DeleteAt = DateTime.Now,
            //    Email = Guid.NewGuid().ToString(),
            //    EmailConfirmed = false,
            //    ModifyAt = DateTime.Now,
            //    PasswordHash = "",
            //    PhoneConfirmed = false,
            //    RoleId = 1,
            //    Salt = "",
            //    TwoFactorEnabled = false,
            //    Username = Guid.NewGuid().ToString()
            //});

            //var users = await _qs.For<User>().Get.ToListAsync();

            //var en = users.AsEnumerable();

            //foreach (var useren in en)
            //{
            //    useren.Username = "Test Change Field";
            //}

            

            await _qs.TransactionAsync(async (qs, ctn) => {
                await _qs.For<User>().Insert.InsertWithObjectAsync(new User
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
                    Username = Guid.NewGuid().ToString()
                }, ctn);

                await _qs.For<User>().Insert.InsertWithObjectAsync(new User
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
                    Username = Guid.NewGuid().ToString()
                }, ctn);

                await _qs.For<User>().Insert.InsertWithObjectAsync(new User
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
                    Username = Guid.NewGuid().ToString()
                }, ctn);
            });

            //var ewnum = roles.Select(x => x.Name);

            //var users2 = await _qs.For<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToListAsync();

            return Ok();
        }
    }
}