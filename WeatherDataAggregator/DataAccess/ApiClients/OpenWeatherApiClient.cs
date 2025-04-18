using WeatherDataAggregator.DataAccess.ApiClients;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator;

public class OpenWeatherApiClient : IDisposable, IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private const string _urlBase = @"https://api.openweathermap.org/data/2.5/weather";

    public OpenWeatherApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetWeatherData(Country country, string apiKey)
    {
        string urlParameters = $"?lat={country.Latitude}&lon={country.Longitude}&appid={apiKey}&units=metric";
        string fullUrl = $"{_urlBase}{urlParameters}";
        var response = await _httpClient.GetAsync(fullUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
