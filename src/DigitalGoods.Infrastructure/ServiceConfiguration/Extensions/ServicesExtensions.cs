using Microsoft.Extensions.DependencyInjection;
using DigitalGoods.Core.Services;

namespace DigitalGoods.Infrastructure.ServiceConfiguration.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<OfferService>();
            services.AddTransient<MediaService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<TagService>();
        }
    }
}
