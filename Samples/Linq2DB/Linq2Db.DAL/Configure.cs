using DALQueryChain.Linq2Db;
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

            services.AddQueryChain(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
