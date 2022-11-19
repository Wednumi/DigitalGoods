using Microsoft.Extensions.DependencyInjection;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Infrastructure.Data;
using DigitalGoods.Infrastructure.DataAccess;
using DigitalGoods.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace DigitalGoods.Infrastructure.ServiceConfiguration.Extensions
{
    public static class AccountExtensions
    {
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
