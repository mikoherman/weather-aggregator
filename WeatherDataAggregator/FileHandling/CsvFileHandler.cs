using System.Globalization;
using CsvHelper;

namespace WeatherDataAggregator.FileHandling;

public class CsvFileHandler<T> : IFileHandler<IEnumerable<T>>
{
    public void WriteToAFile(string path, IEnumerable<T> data)
    {
        using var writer = new StreamWriter(path);
        using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(data);
    }

    public IEnumerable<T>? ReadFromAFile(string path)
    {
        if (!File.Exists(path))
            return default(IEnumerable<T>);

        using var reader = new StreamReader(path);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csvReader.GetRecords<T>();
    }
}
