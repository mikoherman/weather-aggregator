using System.Collections.ObjectModel;
using WeatherDataAggregator.DTOs;
using WeatherDataAggregator.FileHandling;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess;

/// <summary>
/// A decorator for <see cref="IWeatherDataProvider"/> that adds functionality to export weather data to a file.
/// </summary>
/// <remarks>
/// This class wraps an existing <see cref="IWeatherDataProvider"/> and extends its behavior by saving the fetched weather data
/// to a file using the provided <see cref="IFileHandler{T}"/>.
/// </remarks>
public class FileExportingWeatherDataProvider : IWeatherDataProvider
{
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly IFileHandler<IEnumerable<CountryWeatherDto>> _fileHandler;
    private readonly Func<KeyValuePair<Country, WeatherData>, CountryWeatherDto> _mapper;
    private readonly string _fileName;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileExportingWeatherDataProvider"/> class.
    /// </summary>
    /// <param name="weatherDataProvider">The underlying <see cref="IWeatherDataProvider"/> to fetch weather data.</param>
    /// <param name="fileHandler">The <see cref="IFileHandler{T}"/> used to write weather data to a file.</param>
    /// <param name="mapper">
    /// A function that maps a <see cref="KeyValuePair{TKey, TValue}"/> of <see cref="Country"/> and <see cref="WeatherData"/>
    /// to a <see cref="CountryWeatherDto"/> for file export.
    /// </param>
    /// <param name="fileName">The name of the file where the weather data will be saved.</param>
    public FileExportingWeatherDataProvider(IWeatherDataProvider weatherDataProvider, IFileHandler<IEnumerable<CountryWeatherDto>> fileHandler, Func<KeyValuePair<Country, WeatherData>, CountryWeatherDto> mapper, string fileName)
    {
        _weatherDataProvider = weatherDataProvider;
        _fileHandler = fileHandler;
        _mapper = mapper;
        _fileName = fileName;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="FileExportingWeatherDataProvider"/> class with a default mapper.
    /// </summary>
    /// <param name="weatherDataProvider">The underlying <see cref="IWeatherDataProvider"/> to fetch weather data.</param>
    /// <param name="fileHandler">The <see cref="IFileHandler{T}"/> used to write weather data to a file.</param>
    /// <param name="fileName">The name of the file where the weather data will be saved.</param>
    public FileExportingWeatherDataProvider(IWeatherDataProvider weatherDataProvider,
        IFileHandler<IEnumerable<CountryWeatherDto>> fileHandler, string fileName) : 
        this(weatherDataProvider, fileHandler, DefaultMapper, fileName) { }
    /// <summary>
    /// The default mapper function for converting weather data to <see cref="CountryWeatherDto"/>.
    /// </summary>
    /// <param name="kvp">
    /// A <see cref="KeyValuePair{TKey, TValue}"/> where the key is a <see cref="Country"/> and the value is a <see cref="WeatherData"/>.
    /// </param>
    /// <returns>A <see cref="CountryWeatherDto"/> representing the mapped weather data.</returns>
    private static CountryWeatherDto DefaultMapper(KeyValuePair<Country, WeatherData> kvp)
    {
        return new CountryWeatherDto(
            kvp.Key.Name,
            kvp.Value.WeatherDescription,
            kvp.Value.Temperature,
            kvp.Value.FeelsLikeTemperature,
            kvp.Value.Humidity,
            kvp.Value.WindSpeed,
            kvp.Value.Pressure
        );
    }
    /// <summary>
    /// Retrieves weather data for the specified countries and saves the result to a file.
    /// </summary>
    /// <param name="countries">The collection of <see cref="Country"/> objects for which weather data is requested.</param>
    /// <param name="apiKey">The API key used to authenticate with the weather data provider.</param>
    /// <returns>
    /// A <see cref="ReadOnlyDictionary{TKey, TValue}"/> where the key is a <see cref="Country"/> and the value is the corresponding <see cref="WeatherData"/>.
    /// </returns>
    /// <remarks>
    /// This method fetches weather data using the wrapped <see cref="IWeatherDataProvider"/> and then maps the results
    /// to <see cref="CountryWeatherDto"/> objects for export to a file.
    /// </remarks>
    public async Task<ReadOnlyDictionary<Country, WeatherData>> GetWeatherDataForCountriesAsync(IEnumerable<Country> countries, string? apiKey)
    {
        var resultDictionary = await _weatherDataProvider
            .GetWeatherDataForCountriesAsync(countries, apiKey);
        var dtosForCsv = resultDictionary.Select(kvp => _mapper(kvp));
        _fileHandler.WriteToAFile(_fileName, dtosForCsv);

        return resultDictionary;
    }
}
