using LinqToDB;
using ManualTest.Linq2Db.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Core.Fixtures.Contexts.Linq2Db;

namespace Tests.Core.TestCases
{
    public abstract class Linq2dbDatabaseTestCase : IDisposable
    {
        private readonly string _databaseName;
        private readonly Linq2DbDatabaseFixture _databaseFixture;

        public Linq2dbDatabaseTestCase(Linq2DbDatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;

            var id = Guid.NewGuid().ToString("n");
            _databaseName = $"DQC_Test_{id}";

            using var tmplConnection = new NpgsqlConnection(_databaseFixture.Connection);
            tmplConnection.Open();
            using var cmd = new NpgsqlCommand($"CREATE DATABASE {_databaseName} WITH TEMPLATE {_databaseFixture.TemplateDatabaseName}", tmplConnection);
            cmd.ExecuteNonQuery();

            var connection = $"User ID=postgres;Password=admin;Host=localhost;Port=5432;Database={_databaseName}";

            var options = new DataOptions();
            options.UsePostgreSQL(connection);

            DbContext = new TestContext(new DataOptions<TestContext>(options));
        }

        public void Dispose()
        {
            using var tmplConnection = new NpgsqlConnection(_databaseFixture.Connection);
            tmplConnection.Open();
            using var cmd = new NpgsqlCommand($"DROP DATABASE {_databaseName}", tmplConnection);
            cmd.ExecuteNonQuery();
        }

        public TestContext DbContext { get; }
    }
}
