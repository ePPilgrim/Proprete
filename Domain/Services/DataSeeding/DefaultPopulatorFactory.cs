using AutoMapper;
using Proprette.Domain.Data.Entities;

namespace Proprette.Domain.Services.DataSeeding
{
    internal class DefaultPopulatorFactory : IPopulatorFactory
    {
        private readonly IEntityFactory<Warehouse> warehouseFactory;
        private readonly IMapper mapper;

        public DefaultPopulatorFactory(IMapper mapper, IEntityFactory<Warehouse> warehouseFactory)
        {
            this.warehouseFactory = warehouseFactory ?? throw new ArgumentNullException(nameof(warehouseFactory));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public IPopulateTable<T> CreateWarehousePopulator<T>() where T : class
        {
            return new PopulateWarehouse<T>(warehouseFactory, mapper);
        }
    }
}
