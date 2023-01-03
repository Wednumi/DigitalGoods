using AutoMapper;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DigitalGoods.Core
{
    public static class ConfigureServices
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
            services.AddTransient<OrderService>();
            services.AddTransient<PaymentManager>();
            services.AddTransient<PaymentRegistrator>();

            services.AddScoped<IRollBackContainer, RollBackContainer>();
        }
    }
}
