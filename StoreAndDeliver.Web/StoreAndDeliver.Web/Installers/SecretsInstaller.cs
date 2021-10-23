using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAndDeliver.Web.Options;

namespace StoreAndDeliver.Web.Installers
{
    public class SecretsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlConfigOptions>(configuration.GetSection("ConnectionStrings:Default"));
        }
    }
}
