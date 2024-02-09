using Linq2Db.DAL.Context;
using LinqToDB;
using LinqToDB.Data;

namespace DALQueryChain.Tests.Linq2DB.Common.Fixtures.Objects
{
    public class Linq2DbDatabaseFixture
    {
        private readonly DataConnection _context;

        public Linq2DbDatabaseFixture()
        {
            TemplateDatabaseName = "test";
            Connection = $"User ID=postgres;Password=admin;Host=localhost;Port=5432;Database={TemplateDatabaseName}";

            var options = new DataOptions();
            options = options.UsePostgreSQL(Connection);

            _context = new TestContext(new DataOptions<TestContext>(options));

            _context.Close();
        }

        public void Dispose()
        {
            //_context.Close();
        }

        public string TemplateDatabaseName { get; }
        public string Connection { get; }
    }
}
