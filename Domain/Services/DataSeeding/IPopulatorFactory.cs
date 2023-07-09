namespace Proprette.Domain.Services.DataSeeding
{
    public interface IPopulatorFactory
    {
        IPopulateTable<T> CreateWarehousePopulator<T>() where T : class;
    }
}
