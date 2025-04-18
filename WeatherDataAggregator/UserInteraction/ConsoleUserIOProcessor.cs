using System.Collections.ObjectModel;
using WeatherDataAggregator.Models;
using WeatherDataAggregator.UserInteraction.TablePrinter;
using WeatherDataAggregator.Utilities;

namespace WeatherDataAggregator.UserInteraction;

public class ConsoleUserIOProcessor
{
    private readonly IConsoleUserInteractor _userInteractor;
    private readonly IConsoleWeatherDataTablePrinter _weatherDataTablePrinter;

    public ConsoleUserIOProcessor(IConsoleUserInteractor userInteractor, 
        IConsoleWeatherDataTablePrinter weatherDataTablePrinter)
    {
        _userInteractor = userInteractor;
        _weatherDataTablePrinter = weatherDataTablePrinter;
    }

    private void DisplayAllContinents()
    {
        var allContinentsToDisplay = 
            ContinentEnumExtensions.GetContinentListAsString();
        _userInteractor.DisplayMessage(allContinentsToDisplay);
    }

    public Continent PromptUserForContinent()
    {
        DisplayAllContinents();
        string? userInput;
        int continentEnumId;
        do
        {
            _userInteractor.DisplayMessage("Please pick Continent Number");
            userInput = _userInteractor.Prompt();
        } while (
        string.IsNullOrEmpty(userInput) ||
        !int.TryParse(userInput, out continentEnumId) ||
        !Enum.IsDefined(typeof(Continent), continentEnumId));
        return (Continent) continentEnumId;
    }

    public void DisplayWeatherDataForCountries(
        ReadOnlyDictionary<Country, WeatherData> weathersInCountries) =>
        _weatherDataTablePrinter.PrintTable(weathersInCountries);
}
