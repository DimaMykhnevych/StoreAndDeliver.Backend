using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAndDeliver.Web.Installers;
using System;
using System.Linq;

namespace StoreAndDeliver.Web.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
