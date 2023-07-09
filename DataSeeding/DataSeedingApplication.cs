using Microsoft.Build.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proprette.DataSeeding.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proprette.DataSeeding 
{
    internal class DataSeedingApplication : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IHostEnvironment environment;
        private readonly MainServiceFactory mainServiceFactory;

        public DataSeedingApplication(IServiceProvider serviceProvider,
            IConfiguration configuration,
            IHostApplicationLifetime hostApplicationLifetime,
            IHostEnvironment environment,
            MainServiceFactory mainServiceFactory)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
            this.hostApplicationLifetime = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.mainServiceFactory = mainServiceFactory ?? throw new ArgumentNullException(nameof(mainServiceFactory));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Application has finished!!!!");
        }

        private void OnStarted()
        {
            var appOption = configuration["appopt"];
            if (string.IsNullOrEmpty(appOption))
            {
                Console.WriteLine("No app option is set so default option is used.");
                appOption = "p";
            }
            var dir = configuration["ModelRootDir"];
            if (string.IsNullOrEmpty(dir))
            {
                Console.WriteLine("No root directory is set so environment rood directory is used.");
                dir = environment.ContentRootPath;
            }
            dir = @"C:\Users\demyd\Practice\Proprette\DataSeeding\resources";
            if (!Directory.Exists(dir))
            {
                throw new ArgumentException($"No directory {dir} exist");
            }
            using (serviceProvider.CreateScope()) ;
            var service = mainServiceFactory.Create(appOption);
            service.Run(dir).Wait();
        }
    }
}
