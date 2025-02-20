﻿using client.service.ui.api.service.clientServer;
using client.service.ui.api.service.webServer;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace client.service.ui.api.service
{
    public static class ServiceCollectionExtends
    {
        public static ServiceCollection AddUI(this ServiceCollection services, Assembly[] assemblys)
        {
            services.AddSingleton<Config>();
            services.AddClientServer(GetAddemblys(assemblys));
            services.AddWebServer();

            return services;
        }
        public static ServiceProvider UseUI(this ServiceProvider services, Assembly[] assemblys)
        {
            services.UseClientServer(GetAddemblys(assemblys));
            services.UseWebServer();
            
            return services;
        }

        private static Assembly[] GetAddemblys(Assembly[] assemblys)
        {
            return new Assembly[] {
            }.Concat(assemblys).ToArray();
        }
    }
}
