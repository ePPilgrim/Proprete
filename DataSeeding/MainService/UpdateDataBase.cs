using Microsoft.Extensions.Logging;
using Proprette.DataSeeding.DataSource.Models;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.DataSeeding.MainService
{
    internal sealed class UpdateDataBase : BaseMainService
    {
        private readonly IFileReader<IFileToModel> fileReader;
        private readonly IPopulatorFactory populatorFactory;

        private IDictionary<string, string> tableToModelMapping = new Dictionary<string, string>();

        public UpdateDataBase(IFileReader<IFileToModel> fileReader,
                              IPopulatorFactory populatorFactory,
                              ILogger<IMainService> logger) : base(logger)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.populatorFactory = populatorFactory ?? throw new ArgumentNullException(nameof(populatorFactory));
            createTableToModelMapping();
        }

        protected override string ServiceName => "UpdateDataBase";

        protected override async Task runService(string rootFileDirectory)
        {
            logger.LogInformation($"Root directory - {rootFileDirectory}.");
            var warehouseFile = Directory.GetFiles(rootFileDirectory)
                .Where(x => Path.GetExtension(x) == ".csv")
                .Where(x => Path.GetFileNameWithoutExtension(x).ToLower() == tableToModelMapping["Warehouse"].ToLower())
                .FirstOrDefault();
            if (warehouseFile == null)
            {
                logger.LogError("No file for table seeding were found");
                return;
            }
            await populateWarehouse(warehouseFile);
        }

        private void createTableToModelMapping()
        {
            tableToModelMapping = new Dictionary<string, string>();
            tableToModelMapping["Warehouse"] = "FileToWarehouse";
        }

        private async Task populateWarehouse(string path)
        {
            IEnumerable<FileToWarehouse>? data = fileReader.ReadAll(path).Select(x=>(FileToWarehouse)x);
            if (data == null)
            {
                logger.LogError("No data is uploaded from the file to memory");
                return;
            }
            await populatorFactory.CreateWarehousePopulator<FileToWarehouse>().UpdateOrInsert(data);
        }
    }
}
