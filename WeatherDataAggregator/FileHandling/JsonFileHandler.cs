using System.Text.Json;

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

        var json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
            return default(T);
        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex) when (ex is JsonException || ex is NotSupportedException)
        {
            // TODO add Logging
            throw;
        }
    }

    public void WriteToAFile(string path, T data)
    {
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(path, json);
    }
}








