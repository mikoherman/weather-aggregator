using System.Collections.ObjectModel;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.UserInteraction.TablePrinter;

public interface IConsoleWeatherDataTablePrinter
{
    void PrintTable(ReadOnlyDictionary<Country, WeatherData> weathersInCountries);
}