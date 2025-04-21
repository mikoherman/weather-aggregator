using System.Text.Json;
using Serilog;

namespace WeatherDataAggregator.FileHandling;

public class JsonFileHandler<T> : IFileHandler<T>
{
    /// <summary>
    /// Reads data from a JSON file and deserializes it into an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="path">The path to the JSON file.</param>
    /// <returns>
    /// An object of type <typeparamref name="T"/> if the file exists and contains valid JSON data;
    /// otherwise, <c>default(T)</c>.
    /// </returns>
    /// <exception cref="JsonException">
    /// Thrown if the JSON data in the file is invalid or cannot be deserialized into the specified type.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Thrown if the type <typeparamref name="T"/> is not supported for deserialization.
    /// </exception>
    /// <remarks>
    /// If the file does not exist or is empty, the method returns <c>default(T)</c>.
    /// </remarks>
    public T? ReadFromAFile(string path)
    {
        if (!File.Exists(path))
            return default(T);
        string? json = null;
        try
        {
            json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json))
                return default(T);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (ArgumentException ex)
        {
            Log.Error($"{path} is an invalid string for a path parameter. Exception: {ex}");
            throw;
        }
        catch (IOException ex)
        {
            Log.Error($"An I/O error has occured while opening the file {path}. Exception: {ex}");
            throw;
        }
        catch (JsonException ex)
        {
            Log.Error($"JSON string: {json ?? "-"} read from {path} was in an invalid format. Exception: {ex}");
            throw;
        }
    }
    public void WriteToAFile(string path, T data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
        }
        catch (NotSupportedException ex)
        {
            Log.Error($"Failed to serialize type {typeof(T).Name} to JSON while writing to file '{path}'. Exception: {ex}.");
            throw;
        }
        catch(PathTooLongException ex)
        {
            Log.Error($"The specified path, file name, or both exceed the system-defined maximum length. For path: {path}. Exception: {ex}");
            throw;
        }
        catch (IOException ex)
        {
            Log.Error($"An I/O error has occured while opening the file {path}. Exception: {ex}");
            throw;
        }
    }
}