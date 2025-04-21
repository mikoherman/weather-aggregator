using System.Collections.ObjectModel;
using WeatherDataAggregator.DataAccess.DataProviders;
using WeatherDataAggregator.Models;
using WeatherDataAggregator.UserInteraction;

namespace WeatherDataAggregator;

public class WeatherDataAggregatorApp
{
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly ICountryDataProvider _countryDataProvider;
    private readonly IConsoleUserIOProcessor _userIOProcessor;

    public WeatherDataAggregatorApp(IWeatherDataProvider weatherDataProvider,
        ICountryDataProvider countryDataProvider, IConsoleUserIOProcessor consoleUserIOProcessor)
    {
        _weatherDataProvider = weatherDataProvider;
        _countryDataProvider = countryDataProvider;
        _userIOProcessor = consoleUserIOProcessor;
    }
    public async Task Run(string? apiKey)
    {
        Continent continent = _userIOProcessor.PromptUserForContinent();
        ReadOnlyCollection<Country> countryCollection =
            await _countryDataProvider.GetCountriesByContinentAsync(continent);
        ReadOnlyDictionary<Country, WeatherData> countriesWithWeathers =
            await _weatherDataProvider
            .GetWeatherDataForCountriesAsync(countryCollection, apiKey);
        _userIOProcessor.DisplayWeatherDataForCountries(countriesWithWeathers);
    }
}