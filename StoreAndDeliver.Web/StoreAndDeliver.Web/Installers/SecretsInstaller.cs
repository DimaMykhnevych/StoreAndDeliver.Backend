using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAndDeliver.BusinessLayer.Options;
using StoreAndDeliver.Web.Options;

namespace StoreAndDeliver.Web.Installers
{
    public class SecretsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlConfigOptions>(configuration.GetSection("ConnectionStrings:Default"));
            services.Configure<EmailServiceOptions>(configuration.GetSection("EmailServiceOptions"));
            services.Configure<ExchangeRatesApiOptions>(configuration.GetSection("ExchangeRatesApiOptions"));
            services.Configure<AzureStorageAccountOptions>(configuration.GetSection("AzureStorageAccountOptions"));
        }
    }
}
