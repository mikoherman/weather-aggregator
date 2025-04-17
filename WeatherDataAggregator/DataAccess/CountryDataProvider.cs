using System.Collections.ObjectModel;
using System.Text.Json;
using WeatherDataAggregator.DataAccess.ApiClients;
using WeatherDataAggregator.DTOs.RestCountiresDTOs;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess;

public class CountryDataProvider
{
    private readonly ICountriesApiClient _countriesApiClient;

    public CountryDataProvider(ICountriesApiClient countriesApiClient)
    {
        _countriesApiClient = countriesApiClient;
    }

    public async Task<ReadOnlyCollection<Country>> GetCountriesByContinentAsync(Continent continent)
    {
        var dtoJson = await _countriesApiClient.FetchData(continent);
        var dtoList = JsonSerializer.Deserialize<List<CountryDto>>(dtoJson);

        if (dtoList is not null && dtoList.Count != 0)
        {
            return new ReadOnlyCollection<Country>(
                dtoList.Select(dtoCountry => new Country()
                {
                    Name = dtoCountry.Name.Common,
                    Latitude = dtoCountry.LatLng[0],
                    Longitude = dtoCountry.LatLng[1]
                }).ToList());
        }
        else
            throw new JsonException("Failed to deserialize JSON into CountryDto. The result was null.");
    }
}








