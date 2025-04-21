using System.Globalization;
using CsvHelper;
using Serilog;

namespace WeatherDataAggregator.FileHandling;
/// <summary>
/// A file handler for reading and writing data in CSV format.
/// </summary>
/// <typeparam name="T">The type of data to be serialized and deserialized.</typeparam>
/// <remarks>
/// This class uses the <see cref="CsvHelper"/> library to handle CSV file operations.
/// It provides functionality to write a collection of objects to a CSV file and read a collection of objects from a CSV file.
/// The class also includes robust error handling and logging using <see cref="Serilog"/> to capture issues during file operations.
/// </remarks>
public class CsvFileHandler<T> : IFileHandler<IEnumerable<T>>
{
    /// <summary>
    /// Writes a collection of objects to a CSV file.
    /// </summary>
    /// <param name="path">The path to the CSV file where the data will be written.</param>
    /// <param name="data">The collection of objects to write to the file.</param>
    /// <exception cref="ArgumentException">Thrown if the path is null or empty.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown if the specified path is invalid.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if access to the path is denied.</exception>
    /// <exception cref="PathTooLongException">Thrown if the specified path exceeds the system-defined maximum length.</exception>
    /// <exception cref="CsvHelperException">Thrown if an error occurs while writing the CSV file.</exception>
    /// <exception cref="IOException">Thrown if an I/O error occurs during the file operation.</exception>
    /// <remarks>
    /// This method uses the <see cref="CsvHelper.CsvWriter"/> to serialize the collection of objects into CSV format.
    /// </remarks>
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
    /// <summary>
    /// Reads a collection of objects from a CSV file.
    /// </summary>
    /// <param name="path">The path to the CSV file to read from.</param>
    /// <returns>
    /// A collection of objects of type <typeparamref name="T"/> if the file exists and contains valid CSV data. <c>null</c> if the file doesn't exists.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if the path is null or empty.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown if the specified path is invalid.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if access to the path is denied.</exception>
    /// <exception cref="PathTooLongException">Thrown if the specified path exceeds the system-defined maximum length.</exception>
    /// <exception cref="CsvHelperException">Thrown if an error occurs while reading the CSV file.</exception>
    /// <exception cref="IOException">Thrown if an I/O error occurs during the file operation.</exception>
    /// <remarks>
    /// This method uses the <see cref="CsvHelper.CsvReader"/> to deserialize the CSV file into a collection of objects.
    /// </remarks>
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
    /// <summary>
    /// Logs details of exceptions that occur during file operations.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="path">The path to the file being operated on.</param>
    /// <param name="operation">The operation being performed (e.g., "write to a csv file").</param>
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