using DigitalGoods.Infrastructure.ServiceConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DigitalGoods.Infrastructure.DataAccess;
using DigitalGoods.Core;

namespace DigitalGoods.IntegrationTests.Configuration
{
    public class ConfiguredTest 
    {
        public IServiceProvider ServiceProvider { get; set; }

        public ConfiguredTest()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

            var services = new ServiceCollection();
            services.AddCoreServices();
            services.AddInfrastructureServices(config, connectionStringName: "Tests");
            services.AddHttpContextAccessor();

            services.AddLogging();

            ServiceProvider = services.BuildServiceProvider();

            ResetDataBase();
        }

        protected void ResetDataBase()
        {
            var dbContext = ServiceProvider.GetRequiredService<ApplicationContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

        }
    }
}
