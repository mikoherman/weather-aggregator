using System.Collections.ObjectModel;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess.ApiClients;

public class RestCountriesApiClient : ICountriesApiClient, IDisposable
{
    private const string _urlBase = @"https://restcountries.com/v3.1/region/";
    private readonly HttpClient _httpClient;
    private readonly ReadOnlyDictionary<Continent, string> _continentSelector =
        new ReadOnlyDictionary<Continent, string>(new Dictionary<Continent, string>()
        {
            [Continent.Europe] = "europe",
            [Continent.Asia] = "asia",
            [Continent.Africa] = "africa",
            [Continent.NorthAmerica] = "north america",
            [Continent.SouthAmerica] = "south america",
            [Continent.Oceania] = "oceania"
        });

    public RestCountriesApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> FetchData(Continent continent)
    {
        string fullUrl = $"{_urlBase}{_continentSelector[continent]}";
        var response = await _httpClient.GetAsync(fullUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
