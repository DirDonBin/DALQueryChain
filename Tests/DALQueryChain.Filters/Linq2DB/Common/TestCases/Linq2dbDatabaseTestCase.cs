using DALQueryChain.Tests.Linq2DB.Common.Fixtures.Objects;
using Linq2Db.DAL.Context;
using LinqToDB;
using ManualTest.Linq2Db.Context;
using Npgsql;

namespace DALQueryChain.Tests.Linq2DB.Common.TestCases
{
    public abstract class Linq2dbDatabaseTestCase : IDisposable
    {
        private readonly string _databaseName;
        private readonly Linq2DbDatabaseFixture _databaseFixture;

        public Linq2dbDatabaseTestCase(Linq2DbDatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;

            var id = Guid.NewGuid().ToString("n").ToLower();
            _databaseName = $"dqc_test_{id}";

            using var tmplConnection = new NpgsqlConnection(_databaseFixture.Connection);
            tmplConnection.Open();
            using var cmd = new NpgsqlCommand($"CREATE DATABASE {_databaseName} WITH TEMPLATE {_databaseFixture.TemplateDatabaseName}", tmplConnection);
            cmd.ExecuteNonQuery();

            var connection = $"User ID=postgres;Password=admin;Host=localhost;Port=5432;Database={_databaseName}";

            var options = new DataOptions();
            options = options.UsePostgreSQL(connection);

            DbContext = new TestContext(new DataOptions<TestContext>(options));
        }

        public void Dispose()
        {
            DbContext.Dispose();

            using var tmplConnection = new NpgsqlConnection(_databaseFixture.Connection);
            tmplConnection.Open();

            var sql = $@"SELECT 
                            pg_terminate_backend(pid) 
                        FROM 
                            pg_stat_activity 
                        WHERE 
                            -- don't kill my own connection!
                            pid <> pg_backend_pid()
                            -- don't kill the connections to other databases
                            AND datname = '{_databaseName}'
                        ;";

            using var cmd1 = new NpgsqlCommand(sql, tmplConnection);
            cmd1.ExecuteNonQuery();

            using var cmd2 = new NpgsqlCommand($"DROP DATABASE {_databaseName}", tmplConnection);
            cmd2.ExecuteNonQuery();
        }

        public TestContext DbContext { get; }
    }
}
