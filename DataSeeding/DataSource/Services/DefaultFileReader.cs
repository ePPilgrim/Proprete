using CsvHelper;
using Proprette.DataSeeding.DataSource.Models;
using System.Globalization;

namespace Proprette.DataSeeding.DataSource.Services
{
    internal class DefaultFileReader : IFileReader<IFileToModel> 
    {
        private readonly IModelLocator<IFileToModel> modelLocator;

        public DefaultFileReader(IModelLocator<IFileToModel> modelLocator)
        {
            this.modelLocator = modelLocator ?? throw new ArgumentNullException(nameof(modelLocator));  
        }

        public IEnumerable<IFileToModel> ReadAll(string path)
        {
            var modelId = this.GetModelId(path);
            var res = getDataFromFile(path, modelId);
            if(res == null)
            {
                modelLocator.ResolveModelLocations();
                res = getDataFromFile(path, modelId) ?? throw new NullReferenceException(nameof(path));
            }
            return res;
        }

        private IEnumerable<IFileToModel>? getDataFromFile(string path, string modelId)
        {
            if (modelLocator.TryToGetTypeByName(modelId, out var modelType))
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var res = csv.GetRecords(modelType);
                    var res3 = res.ToList().Select(x => x);

                    var res2 = (IEnumerable<IFileToModel>)res3;
                    return res2;
                }
            }
            return null;
        }
    }
}
