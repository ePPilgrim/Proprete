using Microsoft.Build.Framework;
using Proprette.DataSeeding.DataSource.Models;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.DataSeeding.MainService
{
    internal class MainServiceFactory
    {
        private readonly IFileReader<IFileToModel> fileReader;
        private readonly IPopulatorFactory populatorFactory;
        public MainServiceFactory(IFileReader<IFileToModel> fileReader,
                                    IPopulatorFactory populatorFactory)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.populatorFactory = populatorFactory ?? throw new ArgumentNullException(nameof(populatorFactory));
        }
        public IMainService Create(string serviceToChoose)
        {
            switch(serviceToChoose)
            {
                case "u":
                    Console.WriteLine("Update and Insert to DB main service is initiated");
                    return new UpdateDataBaseMainService(fileReader, populatorFactory);
                case "d":
                    Console.WriteLine("Delete DB main service is initiated.");
                    return new ClearDataBaseMainService(populatorFactory);
                case "p":
                    Console.WriteLine("Dummy service for creating csv files is initiated.");
                    return new PopulateFileMainService();
                default:
                    throw new NotImplementedException($"No implementation of main service found for this options - {serviceToChoose}");
            }
        }
    }
}
