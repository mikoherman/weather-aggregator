using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.Utilities;

public static class WindSpeedEnumExtensions
{
    /// <summary>
    /// Converts a given wind speed (in meters per second) to a corresponding <see cref="WindSpeed"/> enum value.
    /// </summary>
    /// <param name="windSpeed">The wind speed in meters per second to be categorized.</param>
    /// <returns>
    /// A <see cref="WindSpeed"/> enum value representing the wind speed category, 
    /// ranging from <see cref="WindSpeed.Calm"/> to <see cref="WindSpeed.HuricaneForce"/>.
    /// </returns>
    /// <remarks>
    /// The method uses predefined thresholds based on common wind speed classifications 
    /// (e.g., Beaufort scale) to determine the appropriate category.
    /// Values less than or equal to 0 are categorized as <see cref="WindSpeed.Calm"/>, 
    /// and values greater than 33 are categorized as <see cref="WindSpeed.HuricaneForce"/>.
    /// </remarks>
    public static WindSpeed ConvertToWindSpeed(double windSpeed)
    {
        return windSpeed switch
        {
            <= 0 => WindSpeed.Calm,
            <= 2 => WindSpeed.LightAir,
            <= 3 => WindSpeed.LightBreeze,
            <= 5 => WindSpeed.GentleBreeze,
            <= 8 => WindSpeed.ModerateBreeze,
            <= 11 => WindSpeed.FreshBreeze,
            <= 14 => WindSpeed.StrongBreeze,
            <= 17 => WindSpeed.NearGale,
            <= 21 => WindSpeed.Gale,
            <= 24 => WindSpeed.SevereGale,
            <= 28 => WindSpeed.StormWholeGale,
            <= 33 => WindSpeed.ViolentStorm,
            _ => WindSpeed.HuricaneForce
        };
    }
}
