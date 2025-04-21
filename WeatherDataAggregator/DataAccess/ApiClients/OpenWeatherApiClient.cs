using Serilog;
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
        try
        {
            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode is not null)
            {
                int requestStatusCode = (int)ex.StatusCode;
                string errorType = requestStatusCode switch
                {
                    >= 500 => "Server-side error",
                    >= 400 => "Client-side error",
                    _ => "Unknown error"
                };
                Log.Error($"{errorType} while fetching data from OpenWeather API service for country '{country}' from URL '{_urlBase}'. Status Code: {requestStatusCode}. Exception: {ex}");
            }
            else
            {
                Log.Error($"Unknown error while fetching data from OpenWeather API service for country '{country}' from URL '{_urlBase}'. Exception: {ex}");
            }
            throw;
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
