using Microsoft.Extensions.Logging;
using Proprette.DataSeeding.DataSource.Models;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.DataSeeding.MainService
{
    internal sealed class ClearDataBase : BaseMainService
    {
        private readonly IPopulatorFactory populatorFactory;

        public ClearDataBase(IPopulatorFactory populatorFactory, ILogger<IMainService> logger) : base(logger)
        {
            this.populatorFactory = populatorFactory ?? throw new ArgumentNullException(nameof(populatorFactory));
        }

        protected override string ServiceName => "ClearDataBase";
        protected override async Task runService(string pathToDir)
        {
            var populatorWarehouse = populatorFactory.CreateWarehousePopulator<IFileToModel>();
            await populatorWarehouse.Delete();
        }
    }
}
