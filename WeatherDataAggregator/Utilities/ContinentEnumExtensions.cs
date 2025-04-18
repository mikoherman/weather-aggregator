using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.Utilities;

public static class ContinentEnumExtensions
{
    /// <summary>
    /// Returns a formatted string listing all values of the <see cref="Continent"/> enum,
    /// with their corresponding integer values.
    /// </summary>
    /// <returns>
    /// A string where each line contains the integer value and name of a <see cref="Continent"/> enum member.
    /// </returns>
    public static string GetContinentListAsString() =>
        string.Join(Environment.NewLine, EnumExtensions
            .GetAllValues<Continent>()
            .Select(continent => $"{(int)continent}. {continent}"));
}