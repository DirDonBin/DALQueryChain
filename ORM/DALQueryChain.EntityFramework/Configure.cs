﻿using DALQueryChain.EntityFramework.Builder;
using DALQueryChain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DALQueryChain.EntityFramework
{
    public static class Configure
    {
        internal static List<Type> CachedRepoTypes { get; private set; } = new();

        public static IServiceCollection AddQueryChain(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies is not null)
                CachedRepoTypes.AddRange(assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(IRepository))));

            services.AddScoped(typeof(IDALQueryChain<>), typeof(BuildQuery<>));

            return services;
        }
    }
}
