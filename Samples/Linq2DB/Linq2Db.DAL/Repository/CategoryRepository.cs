using ManualTest.Linq2Db.Context;

namespace Linq2Db.DAL.Repository
{
    public class CategoryRepository : BaseTestContextRepository<Category>
    {
        public CategoryRepository(TestContext context) : base(context)
        {
        }

        public async Task Test()
        {
            var t1 = _context.Transaction;

            _context.BeginTransaction();
            var t2 = _context.Transaction;

            _context.CommitTransaction();

            var t3 = _context.Transaction;

            await _context.BeginTransactionAsync();
            var t4 = _context.Transaction;

            await _context.CommitTransactionAsync();

            var t5 = _context.Transaction;
        }

        protected override Task OnBeforeInsert(CancellationToken ctn = default)
        {
            var t1 = _context.Transaction;
            return base.OnBeforeInsert(ctn);
        }

        protected override Task OnAfterInsert(CancellationToken ctn = default)
        {
            var t1 = _context.Transaction;
            return base.OnAfterInsert(ctn);
        }
    }
}
