namespace WeatherDataAggregator.UserInteraction;

public interface IConsoleUserInteractor
{
    void DisplayMessage(string message);
    string? Prompt();
}