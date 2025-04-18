using System.Text.Json.Serialization;

namespace WeatherDataAggregator.DTOs.OpenWeatherDTOs;

public record WeatherConditionDto(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("icon")] string Icon
);
