using DALQueryChain.Linq2Db.Extensions;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
using ManualTest.Linq2Db.Context;
using Sample.Linq2Db.Controllers;
using static ManualTest.Linq2Db.Context.IdentitySchema;

namespace Sample.Linq2Db.Repositories
{
    public class UserRepository : BaseRepository<TestContext, User>
    {
        private readonly ITestDI _testDI;

        public UserRepository(TestContext context, ITestDI testDI) : base(context)
        {
            _testDI = testDI;
        }

        protected override async Task OnBeforeDelete(CancellationToken ctn = default)
        {
            var deletedEntities = await GetTriggerData(ctn);

            //Some Actions...
        }

        protected override async Task OnAfterDelete(CancellationToken ctn = default)
        {
            var deletedEntities = await GetTriggerData(ctn);

            //Some Actions...
        }

        protected override async Task OnBeforeUpdate(CancellationToken ctn = default)
        {
            var updatedEntities = await GetTriggerData(ctn);

            //Some Actions...
        }

        protected override async Task OnAfterUpdate(CancellationToken ctn = default)
        {
            var updatedEntities = await GetTriggerData(ctn);

            //Some Actions...
        }

        protected override async Task OnBeforeInsert(CancellationToken ctn = default)
        {
            var insertedEntities = await GetTriggerData(ctn);

            //Some Actions...
        }

        protected override async Task OnAfterInsert(CancellationToken ctn = default)
        {
            var insertedEntities = await GetTriggerData(ctn);

            //Some Actions...
        }

        public void Test()
        {
            _testDI.Test();
        }

        //protected override void OnBeforeInsert(User model)
        //{
        //    var users = GetQueryChain<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToList();
        //}

        //protected override async Task OnBeforeInsertAsync(User model, CancellationToken ctn = default)
        //{
        //    var users = await GetQueryChain<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToListAsync();

        //    _query.LoadWith(x => x.Role).ThenLoad(x => x.UsersRoleIdIds).ThenLoad(x => x.Role);
        //}
    }
}
