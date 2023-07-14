using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Proprette.DataSeeding.MainService;

namespace Proprette.DataSeeding
{
    internal class DataSeedingApplication : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IHostEnvironment environment;
        private readonly ILogger<DataSeedingApplication> logger;

        public DataSeedingApplication(IServiceProvider serviceProvider,
            IConfiguration configuration,
            IHostApplicationLifetime hostApplicationLifetime,
            IHostEnvironment environment,
            ILogger<DataSeedingApplication> logger)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
            this.hostApplicationLifetime = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            hostApplicationLifetime.ApplicationStopped.Register(OnStopped); 
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            logger.LogInformation($"Application {environment.ApplicationName} run in the environment - {environment.EnvironmentName}!!!!");
            var appOption = configuration["appopt"];
            if (string.IsNullOrEmpty(appOption))
            {
                logger.LogWarning("No app option is set so default implementation will start.");
            }
            var dir = configuration["ModelRootDir"];
            if (string.IsNullOrEmpty(dir))
            {
                logger.LogWarning("No root directory is set so environment rood directory is used.");
                dir = environment.ContentRootPath;
            }
            if (!Directory.Exists(dir))
            {
                throw new ArgumentException($"No directory {dir} exist");
            }
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var serviceFactory = services.GetService<MainServiceFactory>();
                if(serviceFactory == null)
                {
                    logger.LogError("Service Factory were not resolved!!!!");
                    return;
                }
                var service = serviceFactory.Create(appOption);
                service.Run(dir).Wait();
            }
        }

        private void OnStopped() {
            logger.LogInformation("Application has finished!!!!");
        }
    }
}
