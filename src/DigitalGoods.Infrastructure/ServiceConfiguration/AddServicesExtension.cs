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
using DigitalGoods.Infrastructure.ServiceConfiguration.Extensions;
using DigitalGoods.Infrastructure.ReportWriters;

namespace DigitalGoods.Infrastructure.ServiceConfiguration
{
    public static class AddServicesExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config,
            string connectionStringName = "Default")
        {
            services.AddDbAccess(config, connectionStringName);
            services.AddAccountManager();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IPayPalService, PayPalService>();
            services.AddTransient<IReportWriter, PdfReportWriter>();
        }
    }
}
