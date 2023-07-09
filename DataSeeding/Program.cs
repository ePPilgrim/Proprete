using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Models;
using Proprette.DataSeeding;
using Proprette.Domain.Services.DataSeeding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Proprette.Infrastructure;
using Proprette.Domain;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;
using Proprette.DataSeeding.DataSource.Models;
using static System.Net.Mime.MediaTypeNames;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.DataSeeding.MainService;

var hostBuilder = Host.CreateDefaultBuilder(args);

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
    services.AddScoped<IModelLocator<IFileToModel>, DefaultModelLocator<IFileToModel>>();
    services.AddScoped<IFileReader<IFileToModel>, DefaultFileReader>();
    services.AddScoped<MainServiceFactory>();
    services.AddDomain();
    services.AddAutoMapper(typeof(FileDomainProfile));
});

var host = hostBuilder.Build();
host.Start();






