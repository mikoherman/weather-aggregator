namespace WeatherDataAggregator.Models;

public class WeatherData
{
    public string? WeatherDescription { get; init; }
    public double Temperature { get; init; }
    public double FeelsLikeTemperature { get; init; }
    public double Humidity { get; init; }
    public double WindSpeed { get; init; }
    public double Pressure { get; init; }

    public override string ToString() => $"Weather: {WeatherDescription}, " +
        $"Temperature: {Temperature}°C, " +
        $"Feels Like: {FeelsLikeTemperature}°C, " +
        $"Humidity: {Humidity}%, " +
        $"Wind Speed: {WindSpeed} m/s, " +
        $"Pressure: {Pressure} hPa";
}
