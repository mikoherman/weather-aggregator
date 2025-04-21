using System.Collections.ObjectModel;
using Serilog;
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
        try
        {
            var response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode is not null)
            {
                int requestStatusCode = (int)ex.StatusCode;
                string errorType = requestStatusCode switch
                {
                    >= 500 => "Server-side error",
                    >= 400 => "Client-side error",
                    _ => "Unknown error"
                };
                Log.Error($"{errorType} while fetching data from RestCountries API service for continent '{continent}' from URL '{fullUrl}'. Status Code: {requestStatusCode}. Exception: {ex}");
            }
            else
            {
                Log.Error($"Unknown error while fetching data from RestCountries API service for continent '{continent}' from URL '{fullUrl}'. Exception: {ex}");
            }
            throw;
        }
    }
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
