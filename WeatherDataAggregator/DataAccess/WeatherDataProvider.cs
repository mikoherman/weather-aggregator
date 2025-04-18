using System.Collections.ObjectModel;
using System.Text.Json;
using WeatherDataAggregator.DataAccess.ApiClients;
using WeatherDataAggregator.DTOs.OpenWeatherDTOs;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess;

public class WeatherDataProvider : IWeatherDataProvider
{
    private readonly IWeatherApiClient _weatherApiClient;

    public WeatherDataProvider(IWeatherApiClient weatherApiClient)
    {
        _weatherApiClient = weatherApiClient;
    }

    public async Task<ReadOnlyDictionary<Country, WeatherData>> GetWeatherDataForCountriesAsync(IEnumerable<Country> countries, string? apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentNullException("Provided apiKey was invalid.");

        IEnumerable<Task<KeyValuePair<Country, WeatherData>>> tasks = countries
            .Select(country => GetWeatherDataForCountryAsync(country, apiKey));

        try
        {
            var results = await Task.WhenAll(tasks);
            return new ReadOnlyDictionary<Country, WeatherData>(
                results.ToDictionary(x => x.Key, x => x.Value)
            );
        }
        catch (JsonException)
        {
            //TODO: Add Exception logging
            Console.WriteLine("One or more of the Weather api responses failed to deserialize from JSON");
            throw;
        }
    }

    private async Task<KeyValuePair<Country, WeatherData>> GetWeatherDataForCountryAsync(Country country, string apiKey)
    {
        var stringJson = await _weatherApiClient.GetWeatherData(country, apiKey);
        var weatherApiResponseDto = JsonSerializer.Deserialize<WeatherApiResponseDto>(stringJson);
        if (weatherApiResponseDto is not null)
        {
            return new KeyValuePair<Country, WeatherData>(country, new WeatherData()
            {
                WeatherDescription = weatherApiResponseDto.WeatherConditionDto[0].Description,
                Temperature = weatherApiResponseDto.WeatherMainDto.Temp,
                FeelsLikeTemperature = weatherApiResponseDto.WeatherMainDto.FeelsLike,
                Humidity = weatherApiResponseDto.WeatherMainDto.Humidity,
                WindSpeed = weatherApiResponseDto.Wind.Speed,
                Pressure = weatherApiResponseDto.WeatherMainDto.Pressure
            });
        }
        else
        {
            throw new JsonException("Failed to deserialize JSON into WeatherApiResponseDto. The result was null.");
        }
    }
}
