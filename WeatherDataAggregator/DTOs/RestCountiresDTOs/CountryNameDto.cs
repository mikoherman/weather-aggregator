using System.Text.Json.Serialization;
namespace WeatherDataAggregator.DTOs.RestCountiresDTOs;

public record CountryNameDto(
        [property: JsonPropertyName("common")] string Common
);
