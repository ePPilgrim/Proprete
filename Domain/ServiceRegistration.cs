using Microsoft.Extensions.DependencyInjection;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Models;
using Proprette.Domain.Data.Profiles;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.Domain
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainProfile));
            services.AddScoped<IEntityFactory<Item>, ItemFactory>();    
            services.AddScoped<IEntityFactory<Warehouse>, WarehouseFactory>();  
            services.AddScoped<IPopulateTable<WarehouseDto>, PopulateWarehouse>();
            return services;
        }
    }
}
