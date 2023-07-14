using CsvHelper;
using Microsoft.Extensions.Logging;
using Proprette.DataSeeding.DataSource.Models;
using System.Globalization;

namespace Proprette.DataSeeding.DataSource.Services
{
    internal class DefaultFileReader : IFileReader<IFileToModel> 
    {
        private readonly IModelLocator<IFileToModel> modelLocator;
        private readonly ILogger<DefaultFileReader> logger;

        public DefaultFileReader(IModelLocator<IFileToModel> modelLocator, ILogger<DefaultFileReader> logger)
        {
            this.modelLocator = modelLocator ?? throw new ArgumentNullException(nameof(modelLocator)); 
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<IFileToModel> ReadAll(string path)
        {
            var modelId = this.GetModelId(path);
            var res = getDataFromFile(path, modelId);
            logger.LogInformation($"Resolve model {modelId} with file {path}.");
            if(res == null)
            {
                modelLocator.ResolveModelLocations();
                res = getDataFromFile(path, modelId) ?? throw new NullReferenceException(nameof(path));
            }
            if(res.Where(x=>x == null).Any()){
                throw new Exception($"Some rows of the file are not parsed propperly or there is invalid rows in the file {path}");
            }
            return res.Select(x => (IFileToModel)x);
        }

        private IList<object?>? getDataFromFile(string path, string modelId)
        {
            if (modelLocator.TryToGetTypeByName(modelId, out var modelType))
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var res = csv.GetRecords(modelType).ToList();
                    logger.LogInformation($"Number of records = {res.Count}");
                    return res;
                }
            }
            return null;
        }
    }
}
