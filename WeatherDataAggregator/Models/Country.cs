namespace WeatherDataAggregator.Models;

public class Country
{
    public string Name { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public override string ToString() =>
        $"Country: {Name}, Located at: ({Latitude}, {Longitude})";
}
