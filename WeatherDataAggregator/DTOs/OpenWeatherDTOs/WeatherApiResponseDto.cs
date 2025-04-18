using System.Text.Json.Serialization;

namespace WeatherDataAggregator.DTOs.OpenWeatherDTOs;

public record WeatherApiResponseDto(
    [property: JsonPropertyName("weather")] IReadOnlyList<WeatherConditionDto> WeatherConditionDto,
    [property: JsonPropertyName("main")] WeatherMainDto WeatherMainDto,
    [property: JsonPropertyName("wind")] WindDto Wind
);
