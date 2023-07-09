namespace Proprette.DataSeeding.DataSource.Services
{
    public static class FileServicesUtilities
    {
        public static string GetModelId<T>(this IFileReader<T> _, string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile))
            {
                throw new ArgumentNullException(nameof(pathToFile));
            }
            return Path.GetFileNameWithoutExtension(pathToFile).ToLower();

        }
    }
}
