using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAndDeliver.BusinessLayer.Calculations.Algorithms;
using StoreAndDeliver.BusinessLayer.Clients.ExchangerApiClient;
using StoreAndDeliver.BusinessLayer.Factories;
using StoreAndDeliver.BusinessLayer.Services.AddressService;
using StoreAndDeliver.BusinessLayer.Services.AdminService;
using StoreAndDeliver.BusinessLayer.Services.AuthorizationService;
using StoreAndDeliver.BusinessLayer.Services.CargoRequestService;
using StoreAndDeliver.BusinessLayer.Services.CargoService;
using StoreAndDeliver.BusinessLayer.Services.CargoSessionService;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using StoreAndDeliver.BusinessLayer.Services.CityService;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.BusinessLayer.Services.EmailService;
using StoreAndDeliver.BusinessLayer.Services.EnvironmnetSettingService;
using StoreAndDeliver.BusinessLayer.Services.RequestService;
using StoreAndDeliver.BusinessLayer.Services.StoreService;
using StoreAndDeliver.BusinessLayer.Services.UserService;
using StoreAndDeliver.DataLayer.Builders.CargoRequestQueryBuilder;
using StoreAndDeliver.DataLayer.Builders.CargoSessionQueryBuilder;
using StoreAndDeliver.DataLayer.Builders.CitiesQueryBuilder;
using StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder;
using StoreAndDeliver.DataLayer.Repositories.AddressRepository;
using StoreAndDeliver.DataLayer.Repositories.BackupRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoRequestsRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoSessionRepository;
using StoreAndDeliver.DataLayer.Repositories.CarrierRepository;
using StoreAndDeliver.DataLayer.Repositories.CityRepository;
using StoreAndDeliver.DataLayer.Repositories.EnvironmentSettingReporitory;
using StoreAndDeliver.DataLayer.Repositories.LogsRepository;
using StoreAndDeliver.DataLayer.Repositories.RequestRepository;
using StoreAndDeliver.DataLayer.Repositories.StoreRepository;
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
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IConvertionService, ConvertionService>();
            services.AddTransient<ICargoService, CargoService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<ICarrierService, CarrierService>();
            services.AddTransient<IRequestAlgorithms, RequestAlgorithms>();
            services.AddTransient<ICargoSessionService, CargoSessionService>();
            services.AddTransient<ICargoRequestService, CargoRequestService>();
            services.AddTransient<IAdminService, AdminService>();

            //clients
            services.AddHttpClient<IExchangerApiClient, ExchangerApiClient>();

            // builders
            services.AddTransient<ICitiesQueryBuilder, CitiesQueryBuilder>();
            services.AddTransient<IRequestQueryBuilder, RequestQueryBuilder>();
            services.AddTransient<ICargoSessionQueryBuilder, CargoSessionQueryBuilder>();
            services.AddTransient<ICargoRequestQueryBuilder, CargoRequestQueryBuilder>();

            // repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEnvironmentSettingRepository, EnvironmentSettingRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<ICargoRepository, CargoRepository>();
            services.AddTransient<ICargoRequestsRepository, CargoRequestsRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<ICarrierRepository, CarrierRepository>();
            services.AddTransient<ICargoSessionRepository, CargoSessionRepository>();
            services.AddTransient<IBackupRepository, BackupRepository>();
            services.AddTransient<ILogsRepository, LogsRepository>();
        }
    }
}
