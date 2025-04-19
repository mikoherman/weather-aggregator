namespace WeatherDataAggregator.FileHandling;

public interface IFileHandler<T>
{
    void WriteToAFile(string path, T data);
    T? ReadFromAFile(string path);
}








