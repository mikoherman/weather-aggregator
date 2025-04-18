namespace WeatherDataAggregator.UserInteraction;

public class ConsoleUserInteractor : IConsoleUserInteractor
{
    public void DisplayMessage(string message) =>
        Console.WriteLine(message);
    public string? Prompt() =>
        Console.ReadLine();
}
