using Microsoft.Extensions.Logging;

namespace Proprette.DataSeeding.MainService
{
    internal sealed class PopulateFile : BaseMainService
    {
        private readonly Records records = new Records();

        public PopulateFile(ILogger<IMainService> logger) : base(logger)
        {}

        protected override string ServiceName => "PopulateFile";

        protected override async Task runService(string pathToDir)
        {
            var pathToFile = Path.Combine(pathToDir, "FileToWarehouse.csv");
            await writeToFile(pathToFile, records.WarehouseRecords);
        }

        private async Task writeToFile<T>(string pathToFile, IEnumerable<T> data) where T : class
        {
            try
            {
                // Open the file for writing
                using (StreamWriter sw = new StreamWriter(pathToFile))
                {
                    var propNames = typeof(T).GetProperties().Select(p => p.Name).ToArray();
                    await sw.WriteLineAsync(string.Join(",", propNames));


                    foreach (var row in data)
                    {
                        var propValues = row.GetType().GetProperties().Select(p => p.GetValue(row)).ToArray();
                        await sw.WriteLineAsync(string.Join(",", propValues));
                    }
                }

                Console.WriteLine("Data written to CSV successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to CSV: " + ex.Message);
            }
        }
    }
}
