﻿using DALQueryChain.Linq2Db.Extensions;
using DALQueryChain.Linq2Db.Repositories;
using LinqToDB;
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

        protected override async Task OnBeforeInsertAsync(User model, CancellationToken ctn = default)
        {
            var users = await GetQueryChain<User>().Get.LoadWith(x => x.Role).ThenLoad(x => x!.UsersRoleIdIds).ToListAsync();

            _query.LoadWith(x => x.Role).ThenLoad(x => x.UsersRoleIdIds).ThenLoad(x => x.Role);
        }
    }
}
