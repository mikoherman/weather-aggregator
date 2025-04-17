namespace WeatherDataAggregator
{
    public interface IConsoleUserInteractor
    {
        void DisplayMessage(string message);
        string? Prompt();
    }
}