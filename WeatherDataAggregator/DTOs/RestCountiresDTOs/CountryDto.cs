using System.Text.Json.Serialization;
namespace WeatherDataAggregator.DTOs.RestCountiresDTOs;

public record CountryDto(
        [property: JsonPropertyName("name")] CountryNameDto Name,
        [property: JsonPropertyName("latlng")] IReadOnlyList<double> LatLng
);
