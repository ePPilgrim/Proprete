// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Proprette.DataLayer.Infrastructure;


var hostBuilder = Host.CreateDefaultBuilder(args);

//var connectionString = (args.Length > 0) ? args[0] : Environment.GetEnvironmentVariable("DOTNET_CONNECTIONSTRING");

var connectionString = "server=localhost;user=root;password=1;database=proprettedb1";

hostBuilder.ConfigureServices(services =>
{
    services.AddMariaDbInfrastructure(connectionString);
});

var host = hostBuilder.Build();
host.Start();

Console.WriteLine("Hello, World!");
