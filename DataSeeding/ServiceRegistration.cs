using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proprette.DataSeeding.DataSource.Models;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.DataSeeding.MainService;

namespace Proprette.DataSeeding
{
    internal static class ServiceRegistration
    {
        internal static void AddDataSeedingServices(this IServiceCollection services) 
        {
            services.AddScoped<IModelLocator<IFileToModel>, DefaultModelLocator<IFileToModel>>();
            services.AddScoped<IFileReader<IFileToModel>, DefaultFileReader>();
            services.AddAutoMapper(typeof(FileDomainProfile));
            services.AddTransient<MainServiceFactory>();
        }

        internal static void UpdateDataSeedingConfiguration(this IConfigurationBuilder hostConfigurationBuilder, string[] args)
        {
            hostConfigurationBuilder.Sources.Clear();
            hostConfigurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            hostConfigurationBuilder.AddJsonFile("appsettings.json",
                optional: true,
                reloadOnChange: true);
            hostConfigurationBuilder.AddEnvironmentVariables();
            hostConfigurationBuilder.AddCommandLine(args);
        }
    }
}
