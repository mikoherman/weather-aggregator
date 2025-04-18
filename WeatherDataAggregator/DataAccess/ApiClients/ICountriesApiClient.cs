using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess.ApiClients;

public interface ICountriesApiClient
{
    void Dispose();
    Task<string> FetchData(Continent continent);
}