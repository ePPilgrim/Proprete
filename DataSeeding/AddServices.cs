using Microsoft.Extensions.DependencyInjection;
using Proprette.DataSeeding.DataSource.Models;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.DataSeeding.MainService;

namespace Proprette.DataSeeding
{
    internal static class AddServices
    {
        internal static void AddDataSeedingServices(this IServiceCollection services) 
        {
            services.AddScoped<IModelLocator<IFileToModel>, DefaultModelLocator<IFileToModel>>();
            services.AddScoped<IFileReader<IFileToModel>, DefaultFileReader>();
            services.AddAutoMapper(typeof(FileDomainProfile));
            services.AddScoped<MainServiceFactory>();
        }
    }
}
