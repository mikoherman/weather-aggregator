using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess.ApiClients;

public interface IWeatherApiClient
{
    void Dispose();
    Task<string> GetWeatherData(Country country, string apiKey);
}