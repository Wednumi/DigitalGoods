using AutoMapper;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Services;
using DigitalGoods.Core.Services.CategoryServices;
using DigitalGoods.Core.Services.TagServices;
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
            services.AddTransient<TagService>();
            services.AddTransient<ActivationCodeService>();
            services.AddTransient<IPathGenerator, PathGenerator>();
            services.AddTransient<OfferListEditor>();
            services.AddTransient<OrderService>();
            services.AddTransient<PaymentManager>();
            services.AddTransient<PaymentRegistrator>();
            services.AddTransient<OrderViewingService>();
            services.AddTransient<FeedbackService>();

            services.AddTransient<CategoryTreeViewer>();
            services.AddTransient<CategoryCreator>();
            services.AddTransient<CategoryChildsViewer>();

            services.AddTransient<PossibleTagsViewer>();
            services.AddTransient<TagCreator>();

            services.AddScoped<IRollBackContainer, RollBackContainer>();
        }
    }
}
