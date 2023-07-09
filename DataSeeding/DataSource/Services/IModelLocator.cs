namespace Proprette.DataSeeding.DataSource.Services
{
    public interface IModelLocator<T>
    {
        void ResolveModelLocations();
        bool TryToGetTypeByName(string typeName, out Type? type);
    }
}
