using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proprette.DataLayer.Context;


namespace Proprette.DataLayer.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddMariaDbInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PropretteDbContext>(options =>
        {
            options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 3, 29)), 
                mySqlOptions => mySqlOptions.MigrationsAssembly(
                    typeof(ServiceRegistration).Assembly.FullName)); 
        });

        return services;
    }
}