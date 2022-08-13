using DALQueryChain.Linq2Db.Repositories;
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
            model.DeleteAt = DateTime.Now;
            model.CreateAt = new DateTime(2020, 11, 25);
            model.ModifyAt = new DateTime(2021, 5, 2);
        }
    }
}
