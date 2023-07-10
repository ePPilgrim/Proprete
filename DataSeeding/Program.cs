using Microsoft.EntityFrameworkCore;
using Proprette.DataSeeding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Proprette.Infrastructure;
using Proprette.Domain;
using Microsoft.Extensions.Hosting;
using Proprette.DataSeeding.MainService;
using Microsoft.Extensions.Configuration;

var hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureAppConfiguration(configuration =>
{
    configuration.Sources.Clear();
    configuration.AddJsonFile("appsettings.json",
        optional: true,
        reloadOnChange: true);
    configuration.AddEnvironmentVariables();
    configuration.AddCommandLine(args);
}
);

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()  // Add console logging provider
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);  // Filter logs for database commands
});

hostBuilder.ConfigureServices(services => 
{
    services.AddHostedService<DataSeedingApplication>();
    services.AddSingleton(loggerFactory);
    services.AddInfrastructure("Data Source=C:\\Users\\demyd\\Practice\\Proprette\\API\\Proprette.db");
    services.AddDataSeedingServices();
    services.AddScoped<MainServiceFactory>();
    services.AddDomain();
});


var host = hostBuilder.Build();
host.Start();






