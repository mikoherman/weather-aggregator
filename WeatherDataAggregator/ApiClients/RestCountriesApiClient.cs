using System.Collections.ObjectModel;

namespace WeatherDataAggregator.ApiClients;

public class RestCountriesApiClient
{
    private const string _urlBase = @"https://restcountries.com/v3.1/region/";
    private readonly ReadOnlyDictionary<Continent, string> _continentSelector =
        new ReadOnlyDictionary<Continent, string>(new Dictionary<Continent, string>()
        {
            [Continent.Europe] = "europe",
            [Continent.Asia] = "asia",
            [Continent.Africa] = "africa",
            [Continent.NorthAmerica] = "north america",
            [Continent.SouthAmerica] = "south america",
            [Continent.Australia] = "australia"
        });

    public async Task<string> FetchData(Continent continent)
    {
        string fullUrl = $"{_urlBase}{_continentSelector[continent]}";
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(fullUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
