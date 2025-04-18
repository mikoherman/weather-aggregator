using System.Text.Json.Serialization;

namespace WeatherDataAggregator.DTOs.OpenWeatherDTOs;

public record WeatherMainDto(
        [property: JsonPropertyName("temp")] double Temp,
        [property: JsonPropertyName("feels_like")] double FeelsLike,
        [property: JsonPropertyName("pressure")] int Pressure,
        [property: JsonPropertyName("humidity")] int Humidity
);
