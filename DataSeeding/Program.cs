using Proprette.DataSeeding;
using Microsoft.Extensions.DependencyInjection;
using Proprette.Infrastructure;
using Proprette.Domain;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureServices(services =>
{
    services.AddInfrastructure("Data Source=C:\\Users\\demyd\\Practice\\Proprette\\API\\Proprette.db");
    services.AddDomain();
    services.AddDataSeedingServices();
    services.AddHostedService<DataSeedingApplication>();
});

var host = hostBuilder.Build();
host.Start();






