using Proprette.DataSeeding;
using Microsoft.Extensions.DependencyInjection;
using Proprette.Infrastructure;
using Proprette.Domain;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args);

var connectionString = (args.Length > 0) ? args[0] : Environment.GetEnvironmentVariable("DOTNET_CONNECTIONSTRING");

hostBuilder.ConfigureServices(services =>
{
    //services.AddSqliteInfrastructure("Data Source=C:\\Users\\demyd\\Practice\\Proprette\\API\\Proprette.db");
    services.AddMariaDbInfrastructure(connectionString);
    services.AddDomain();
    services.AddDataSeedingServices();
    services.AddHostedService<DataSeedingApplication>();
});

var host = hostBuilder.Build();
host.Start();






