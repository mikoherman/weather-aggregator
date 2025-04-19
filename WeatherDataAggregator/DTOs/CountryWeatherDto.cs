using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DTOs;

/// <summary>
/// A data transfer object (DTO) representing weather data for a specific country.
/// </summary>
/// <remarks>
/// This DTO is designed to simplify the process of exporting weather data to CSV files.
/// It provides a flat structure that is easy to serialize and write to a file.
/// </remarks>
public record CountryWeatherDto(string CountryName, string? WeatherDescription, double Temperature, double FeelsLikeTemperature, double Humidity, WindSpeed WindSpeed, double Pressure);
