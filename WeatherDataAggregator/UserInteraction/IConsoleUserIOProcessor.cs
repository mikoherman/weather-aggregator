using System.Collections.ObjectModel;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.UserInteraction;

public interface IConsoleUserIOProcessor
{
    void DisplayWeatherDataForCountries(ReadOnlyDictionary<Country, WeatherData> weathersInCountries);
    Continent PromptUserForContinent();
}