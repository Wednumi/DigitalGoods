using AutoMapper;
using DigitalGoods.Core.Extensions;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalGoods.Core
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<OfferService>();
            services.AddTransient<MediaService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<TagService>();
            services.AddTransient<ActivationCodeService>();
            services.AddTransient<IPathGenerator, PathGenerator>();
            services.AddTransient<OfferListEditor>();

            services.AddScoped<IRollBackContainer, RollBackContainer>();
        }
    }
}
