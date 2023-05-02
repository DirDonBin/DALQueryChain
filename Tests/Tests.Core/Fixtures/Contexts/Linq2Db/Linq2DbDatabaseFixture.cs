using DALQueryChain.Linq2Db.Builder;
using LinqToDB;
using LinqToDB.Data;
using ManualTest.Linq2Db.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core.Fixtures.Contexts.Linq2Db
{
    public class Linq2DbDatabaseFixture : IDisposable
    {
        private readonly DataConnection _context;

        public Linq2DbDatabaseFixture()
        {
            TemplateDatabaseName = "test";
            Connection = $"User ID=postgres;Password=admin;Host=localhost;Port=5432;Database={TemplateDatabaseName}";

            var options = new DataOptions();
            options.UsePostgreSQL(Connection);

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
