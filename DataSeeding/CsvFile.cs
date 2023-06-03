using System.Globalization;
using CsvHelper;

namespace Proprette.DataSeeding;

internal class CsvFile<T> 
{
    private readonly string _pathToFile = "";
    public CsvFile(string pathToFile)
    {
        if (string.IsNullOrEmpty(pathToFile) || string.IsNullOrWhiteSpace(pathToFile))
        {
            throw new ArgumentException($"Invalide file path {pathToFile}.");

        }
        _pathToFile = pathToFile.Trim();
    }

    public IList<T> Read()
    {
        using(var reader = new StreamReader(_pathToFile))
        using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<T>().ToList<T>();
        }
    }

    public void Write(IList<T> data)
    {

        try
        {
            // Open the file for writing
            using (StreamWriter sw = new StreamWriter(_pathToFile))
            {
                var propNames = typeof(T).GetProperties().Select(p => p.Name).ToArray();
                sw.WriteLine(string.Join(",", propNames));


                foreach (var row in data)
                {
                    var propValues = row.GetType().GetProperties().Select(p => p.GetValue(row)).ToArray();
                    sw.WriteLine(string.Join(",", propValues));
                }
            }

            Console.WriteLine("Data written to CSV successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to CSV: " + ex.Message);
        }
    }

}
