using Serilog;
using WeatherDataAggregator.DataAccess;
using WeatherDataAggregator.DataAccess.ApiClients;
using WeatherDataAggregator.DataAccess.DataProviders;
using WeatherDataAggregator.DTOs;
using WeatherDataAggregator.FileHandling;
using WeatherDataAggregator.Models;
using WeatherDataAggregator.UserInteraction;
using WeatherDataAggregator.UserInteraction.TablePrinter;

namespace WeatherDataAggregator;

public class Program
{
    static async Task Main(string[] args)
    {
        string? apiKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");
        string logFileName = "log.txt";
        string csvExportFileName = "export.csv";
        string countriesCacheFileName = "countries.json";
        try
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFileName)
                .CreateLogger();
            using var weatherApiClient = new OpenWeatherApiClient(new HttpClient());
            using var restCountriesApiClient = new RestCountriesApiClient(new HttpClient());
            var weatherDataProvider = new FileExportingWeatherDataProvider(
                new WeatherDataProvider(weatherApiClient),
                new CsvFileHandler<CountryWeatherDto>(),
                csvExportFileName);
            var countryDataProvider = new CachedCountryDataProvider(
                new CountryDataProvider(restCountriesApiClient),
                new JsonFileHandler<Dictionary<Continent, IEnumerable<Country>>>(),
                countriesCacheFileName);
            var consoleInteractor = new ConsoleUserInteractor();
            var userIOProcessor = new ConsoleUserIOProcessor(
                consoleInteractor,
                new ConsoleWeatherDataTablePrinter(consoleInteractor));
            var app = new WeatherDataAggregatorApp(weatherDataProvider, countryDataProvider, userIOProcessor);
            await app.Run(apiKey);
        }
        catch (Exception ex)
        {
            Log.Error($"An unexpected error occurred: {ex}");
            Console.WriteLine("An unexpected error occurred. Please check the log file for details.");
            throw;
        }
        Console.WriteLine("Program is finished.");
        Console.ReadKey();
    }
}