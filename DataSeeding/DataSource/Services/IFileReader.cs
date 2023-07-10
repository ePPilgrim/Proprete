namespace Proprette.DataSeeding.DataSource.Services
{
    public interface IFileReader<T>
    {
        IEnumerable<T> ReadAll(string path);
    }
}
