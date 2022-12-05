﻿using DigitalGoods.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DigitalGoods.Infrastructure.ServiceConfiguration.Extensions;

namespace DigitalGoods.Infrastructure.ServiceConfiguration
{
    public static class AddServicesExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config,
            string connectionStringName = "Default")
        {
            services.AddDbAccess(config, connectionStringName);
            services.AddAccountManager();
            services.AddFileManager();

            services.AddCoreServices();
        }
    }
}