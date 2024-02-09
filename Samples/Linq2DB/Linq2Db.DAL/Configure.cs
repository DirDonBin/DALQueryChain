using DALQueryChain.Linq2Db;
using FluentMigrator.Runner;
using Linq2Db.DAL.Context;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using ManualTest.Linq2Db.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Linq2Db.DAL
{
    public static class Configure
    {
        public static IServiceCollection AddLinq2DbDAL(this IServiceCollection services, string connectionString)
        {

            services.AddLinqToDBContext<TestContext>((provider, options) =>
            {
                return options
                    .UsePostgreSQL(connectionString)
                    .UseDefaultLogging(provider);
            });

            var provider = services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Configure).Assembly).For.Migrations().For.EmbeddedResources())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

            provider.GetRequiredService<IMigrationRunner>().MigrateUp();

            services.AddQueryChain(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
