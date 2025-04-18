using System.Collections.ObjectModel;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess;

public interface IWeatherDataProvider
{
    Task<ReadOnlyDictionary<Country, WeatherData>> GetWeatherDataForCountriesAsync(IEnumerable<Country> countries, string? apiKey);
}