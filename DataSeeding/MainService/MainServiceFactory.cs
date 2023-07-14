using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Proprette.DataSeeding.DataSource.Models;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.DataSeeding.MainService
{
    internal class MainServiceFactory
    {
        private readonly IFileReader<IFileToModel> fileReader;
        private readonly IPopulatorFactory populatorFactory;
        private readonly ILogger<IMainService> logger;
        public MainServiceFactory(IFileReader<IFileToModel> fileReader,
                                    IPopulatorFactory populatorFactory,
                                    ILogger<IMainService> logger)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.populatorFactory = populatorFactory ?? throw new ArgumentNullException(nameof(populatorFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IMainService Create(string? serviceToChoose)
        {
            switch(serviceToChoose)
            {
                case "u":
                    return new UpdateDataBase(fileReader, populatorFactory, logger);
                case "d":
                    return new ClearDataBase(populatorFactory, logger);
                case "p":
                    return new PopulateFile(logger);
                default:
                    logger.LogWarning($"No implementation of main service found for this options - {serviceToChoose}");
                    return new Default(logger);
            }
        }
    }
}
