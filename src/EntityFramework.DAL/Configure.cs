using DALQueryChain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EntityFramework.DAL
{
    public static class Configure
    {
        public static IServiceCollection AddEFDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestContext>(options => options.UseNpgsql(connectionString));
            services.AddQueryChain(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
