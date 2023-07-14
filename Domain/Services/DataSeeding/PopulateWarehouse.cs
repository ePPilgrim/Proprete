using AutoMapper;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Services.DataSeeding.Internal;

namespace Proprette.Domain.Services.DataSeeding
{
    internal class PopulateWarehouse<T> : IPopulateTable<T> where T : class
    {
        private readonly IEntityFactory<Warehouse> warehouseFactory;
        private readonly IMapper mapper;
        private readonly IPopulateTableInternal<Warehouse> internalPopulator;

        public PopulateWarehouse(IEntityFactory<Warehouse> warehouseFactory, IMapper mapper)
        {
            this.warehouseFactory = warehouseFactory ?? throw new ArgumentNullException(nameof(warehouseFactory));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            internalPopulator = warehouseFactory.CreatePopulateInternal();
        }

        public async Task Delete()
        {
            await internalPopulator.Delete();   
        }

        public async Task Insert(IEnumerable<T> records)
        {
            var collection = warehouseFactory.CreateCollectionShallow(mapper.Map<IEnumerable<Warehouse>>(records)); 
            await internalPopulator.Insert(collection);
        }

        public async Task UpdateOrInsert(IEnumerable<T> records)
        {
            var collection = warehouseFactory.CreateCollectionShallow(mapper.Map<IEnumerable<Warehouse>>(records));
            await internalPopulator.UpdateOrInsert(collection);
        }
    }
}
