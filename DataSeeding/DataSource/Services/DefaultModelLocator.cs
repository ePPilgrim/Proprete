using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Proprette.DataSeeding.DataSource.Models;
using System.Text.RegularExpressions;

namespace Proprette.DataSeeding.DataSource.Services
{
    internal class DefaultModelLocator<T> : IModelLocator<T>
    {
        private IDictionary<string, Type> mapToType = new Dictionary<string, Type>();
        private ILogger<DefaultModelLocator<T>> logger;

        public DefaultModelLocator(ILogger<DefaultModelLocator<T>> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void ResolveModelLocations()
        {
            var filter = new Regex(@"Proprette*");

            mapToType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => filter.IsMatch(x.GetName().Name))
                .SelectMany(
                    x => x.GetTypes().Where(t => typeof(IFileToModel).IsAssignableFrom(t) && !t.IsInterface),
                    (x, y) => new { Key = y.Name.ToLower(), Value = y })
                .Distinct().ToDictionary(x => x.Key, x => x.Value);
            logger.LogInformation($"Number of models = {mapToType.Count}");
        }

        public bool TryToGetTypeByName(string typeId, out Type? type)
        {
            var res = mapToType.TryGetValue(typeId.ToLower(), out type);
            if(type == null)
            {
                res = false;
            }
            return res;
        }
    }
}
