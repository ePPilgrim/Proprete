using Proprette.DataSeeding.DataSource.Models;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.DataSeeding.MainService
{
    internal class ClearDataBaseMainService : IMainService
    {
        private readonly IPopulatorFactory populatorFactory;

        public ClearDataBaseMainService(IPopulatorFactory populatorFactory)
        {
            this.populatorFactory = populatorFactory ?? throw new ArgumentNullException(nameof(populatorFactory));
        }
        public async Task Run(string pathToDir)
        {
            var populatorWarehouse = populatorFactory.CreateWarehousePopulator<IFileToModel>();
            await populatorWarehouse.Delete();
        }
    }
}
