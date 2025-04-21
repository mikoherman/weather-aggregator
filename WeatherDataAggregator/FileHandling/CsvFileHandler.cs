using System.Globalization;
using CsvHelper;
using Serilog;

namespace WeatherDataAggregator.FileHandling;

public class CsvFileHandler<T> : IFileHandler<IEnumerable<T>>
{
    public void WriteToAFile(string path, IEnumerable<T> data)
    {
        try
        {
            using var writer = new StreamWriter(path);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(data);
        }
        catch (Exception ex)
        {
            LogExceptionDetails(ex, path, "write to a csv file");
            throw;
        }
    }
    public IEnumerable<T>? ReadFromAFile(string path)
    {
        if (!File.Exists(path))
            return default(IEnumerable<T>);
        try
        {
            using var reader = new StreamReader(path);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<T>();
        }
        catch (Exception ex)
        {
            LogExceptionDetails(ex, path, "read from a csv file");
            throw;
        }
    }
    private void LogExceptionDetails(Exception ex, string path, string operation)
    {
        switch (ex)
        {
            case ArgumentException:
                Log.Error($"The path '{path}' was null or empty during {operation}. Exception: {ex}");
                break;
            case DirectoryNotFoundException:
                Log.Error($"The specified path '{path}' was invalid during {operation}. Exception: {ex}");
                break;
            case UnauthorizedAccessException:
                Log.Error($"Access to the path '{path}' is denied during {operation}. Exception: {ex}");
                break;
            case PathTooLongException:
                Log.Error($"The specified path '{path}' exceeds the system-defined maximum length during {operation}. Exception: {ex}");
                break;
            case CsvHelperException:
                Log.Error($"An error occurred while processing the CSV file '{path}' during {operation}. Exception: {ex}");
                break;
            case IOException:
                Log.Error($"An I/O error occurred while {operation} the file '{path}'. Exception: {ex}");
                break;
            default:
                Log.Error($"An unexpected error occurred during {operation} the file '{path}'. Exception: {ex}");
                break;
        }
    }
}