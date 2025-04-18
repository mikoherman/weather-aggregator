using WeatherDataAggregator.Models;
using WeatherDataAggregator.Utilities;

namespace WeatherDataAggregator.UserInteraction;

public class ConsoleUserIOProcessor
{
    private readonly IConsoleUserInteractor _userInteractor;

    public ConsoleUserIOProcessor(IConsoleUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
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
    
}
