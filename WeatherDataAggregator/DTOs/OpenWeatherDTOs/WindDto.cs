using System.Text.Json.Serialization;

namespace WeatherDataAggregator.DTOs.OpenWeatherDTOs;

public record WindDto(
    [property: JsonPropertyName("speed")] double Speed
);