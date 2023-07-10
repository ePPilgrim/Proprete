using Proprette.DataSeeding.DataSource.Models;
using Proprette.DataSeeding.DataSource.Services;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.DataSeeding.MainService
{
    public class UpdateDataBaseMainService : IMainService
    {
        private readonly IFileReader<IFileToModel> fileReader;
        private readonly IPopulatorFactory populatorFactory;

        private IDictionary<string, string> tableToModelMapping = new Dictionary<string, string>();

        public UpdateDataBaseMainService(   IFileReader<IFileToModel> fileReader,
                                            IPopulatorFactory populatorFactory)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.populatorFactory = populatorFactory ?? throw new ArgumentNullException(nameof(populatorFactory));
            createTableToModelMapping();
        }

        public async Task Run(string rootFileDirectory)
        {
            Console.WriteLine($"Root directory - {rootFileDirectory}.");
            var warehouseFile = Directory.GetFiles(rootFileDirectory)
                .Where(x => Path.GetFileNameWithoutExtension(x).ToLower() == tableToModelMapping["Warehouse"].ToLower())
                .FirstOrDefault();
            if (warehouseFile == null)
            {
                Console.WriteLine("No file for table seeding were found");
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
                Console.WriteLine("No data is uploaded from the file to memory");
                return;
            }
            await populatorFactory.CreateWarehousePopulator<FileToWarehouse>().UpdateOrInsert(data);
        }
    }
}
