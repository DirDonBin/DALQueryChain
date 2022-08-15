﻿using DALQueryChain.Linq2Db.Repositories;
using ManualTest.Linq2Db.Context;
using static ManualTest.Linq2Db.Context.IdentitySchema;

namespace Sample.Linq2Db.Repositories
{
    public class UserRepository : BaseRepository<TestContext, User>
    {
        public UserRepository(TestContext context) : base(context)
        {

        }

        protected override void OnBeforeInsert(User model)
        {
            var users = GetQueryChain<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToList();
        }

        protected override async Task OnBeforeInsertAsync(User model)
        {
            var users = await GetQueryChain<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToListAsync();
        }
    }
}
    