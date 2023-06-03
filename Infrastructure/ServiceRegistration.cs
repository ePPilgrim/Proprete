using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proprette.Domain.Service;

namespace Proprette.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PropretteDbContext>(options =>
        {
            options.UseSqlite(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(
                    typeof(ServiceRegistration).Assembly.FullName);
            });
        });

        return services;
    }
}