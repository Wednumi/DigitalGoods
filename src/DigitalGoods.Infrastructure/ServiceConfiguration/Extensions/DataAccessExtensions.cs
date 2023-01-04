using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Interfaces.PaymentServices;
using DigitalGoods.Infrastructure.Data;
using DigitalGoods.Infrastructure.DataAccess;
using DigitalGoods.Infrastructure.Payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalGoods.Infrastructure.ServiceConfiguration.Extensions
{
    public static class DataAccessExtensions
    {
        public static void AddDbAccess(this IServiceCollection services, IConfiguration config,
            string connectionStringName)
        {
            services.AddDbContext<DbContext, ApplicationContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString(connectionStringName));
            }, ServiceLifetime.Transient);

            services.AddTransient<IRepositoryFactory, RepositoryFactory>();
        }

        public static void AddAccountManager(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                options.User.RequireUniqueEmail = true;
            })
               .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IAccountManager, IdentityAccountManager>();
        }
    }
}
