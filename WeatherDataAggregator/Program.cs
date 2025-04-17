using WeatherDataAggregator.DataAccess;
using WeatherDataAggregator.DataAccess.ApiClients;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator;

public class Program
{
    static async Task Main(string[] args)
    {
        var apiFetcher = new RestCountriesApiClient();
        var countryDataProvider = new CountryDataProvider(apiFetcher);
        int counter = 1;
        var countryCollection = (await countryDataProvider
            .GetCountriesByContinentAsync(Continent.Europe))
            .Select(country => $"{counter++}. {country}\n");
        Console.WriteLine(string.Join("", countryCollection));

        Console.WriteLine("Program is finished.");
        Console.ReadKey();
    }
}