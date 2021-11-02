using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAndDeliver.BusinessLayer.Factories;
using StoreAndDeliver.BusinessLayer.Services.AddressService;
using StoreAndDeliver.BusinessLayer.Services.AuthorizationService;
using StoreAndDeliver.BusinessLayer.Services.CityService;
using StoreAndDeliver.BusinessLayer.Services.EmailService;
using StoreAndDeliver.BusinessLayer.Services.EnvironmnetSettingService;
using StoreAndDeliver.BusinessLayer.Services.UserService;
using StoreAndDeliver.DataLayer.Repositories.AddressRepository;
using StoreAndDeliver.DataLayer.Repositories.CityRepository;
using StoreAndDeliver.DataLayer.Repositories.EnvironmentSettingReporitory;
using StoreAndDeliver.DataLayer.Repositories.UserRepository;

namespace StoreAndDeliver.Web.Installers
{
    public class ServiceComponentsDiInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // factories
            services.AddTransient<IAuthTokenFactory, AuthTokenFactory>();

            // services
            services.AddTransient<BaseAuthorizationService, AppUserAuthorizationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEnvironmnetSettingService, EnvironmnetSettingService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ICityService, CityService>();

            // builders

            // repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEnvironmentSettingRepository, EnvironmentSettingRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
        }
    }
}
