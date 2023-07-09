using Proprette.DataSeeding.DataSource.Models;

namespace Proprette.DataSeeding.DataSource.Services
{
    public interface IFileReader<out T>
    {
        IEnumerable<T> ReadAll(string path);
    }
}
