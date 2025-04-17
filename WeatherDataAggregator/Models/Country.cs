namespace WeatherDataAggregator.Models;

public class Country
{
    public string Name { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    public override string ToString() =>
        $"Country: {Name}, Located at: ({Latitude}, {Longitude})";
}
