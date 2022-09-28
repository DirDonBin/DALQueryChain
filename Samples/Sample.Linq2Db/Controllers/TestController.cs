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
            //var users = new List<User>();

            //for (int i = 0; i < 50; i++)
            //{
            //    users.Add(new User
            //    {
            //        AccessFailedCount = 1,
            //        CreateAt = DateTime.Now,
            //        DeleteAt = DateTime.Now,
            //        Email = Guid.NewGuid().ToString(),
            //        EmailConfirmed = false,
            //        ModifyAt = DateTime.Now,
            //        PasswordHash = "",
            //        PhoneConfirmed = false,
            //        RoleId = 1,
            //        Salt = "",
            //        Username = Guid.NewGuid().ToString()
            //    });
            //}

            //await _qs.For<User>().Insert.BulkInsertAsync(users);

            //var tmp = _qs.For<User>().Get.ToList();

            //tmp.ForEach(x => x.Email = Guid.NewGuid().ToString());

            //_qs.For<User>().Update.BulkUpdate(tmp);

            //string? email = null;
            //var tmp = _qs.For<User>().Get.Select(x => x.Email).Max();

            //await _qs.For<User>().Delete.DeleteAsync(x => true);

            var user = new User
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
            };

            var user1 = await _qs.For<User>().Insert.InsertWithObjectAsync(user);

            _qs.Repository<UserRepository>().Test();
            _qs.Repository<RoleRepository>().Test();
            //var rep1 = _qs.For<User>().Repository<UserRepository>();
            //var rep2 = _qs.For<User>().Repository<UserRepository>();
            //var rep3 = _qs.For<User>().Repository<UserRepository>();

            return Ok();
        }

        [HttpGet("TwoTest")]
        public async Task<IActionResult> TestTwo()
        {
            //var users = new List<User>();

            //for (int i = 0; i < 50; i++)
            //{
            //    users.Add(new User
            //    {
            //        AccessFailedCount = 1,
            //        CreateAt = DateTime.Now,
            //        DeleteAt = DateTime.Now,
            //        Email = Guid.NewGuid().ToString(),
            //        EmailConfirmed = false,
            //        ModifyAt = DateTime.Now,
            //        PasswordHash = "",
            //        PhoneConfirmed = false,
            //        RoleId = 1,
            //        Salt = "",
            //        Username = Guid.NewGuid().ToString()
            //    });
            //}

            //await _qs.For<User>().Insert.BulkInsertAsync(users);

            //var tmp = _qs.For<User>().Get.ToList();

            //tmp.ForEach(x => x.Email = Guid.NewGuid().ToString());

            //_qs.For<User>().Update.BulkUpdate(tmp);

            //string? email = null;
            //var tmp = _qs.For<User>().Get.Select(x => x.Email).Max();

            //await _qs.For<User>().Delete.DeleteAsync(x => true);

            var rep = _qs.Repository<UserRepository>();
            var rep2 = _qs.Repository<RoleRepository>();

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