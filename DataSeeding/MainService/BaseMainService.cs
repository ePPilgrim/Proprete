using Microsoft.Extensions.Logging;

namespace Proprette.DataSeeding.MainService
{
    internal class BaseMainService : IMainService
    {
        protected readonly ILogger<IMainService> logger;

        public BaseMainService(ILogger<IMainService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected virtual string ServiceName => "BaseMainService";

        public async Task Run(string pathToDir)
        {
            logger.LogInformation($"{ServiceName} service is started.");
            await runService(pathToDir);
            logger.LogInformation($"{ServiceName} service is stopped.");
        }

        protected virtual async Task runService(string pathToDir)
        {
            await Task.CompletedTask;
        }
    }
}
