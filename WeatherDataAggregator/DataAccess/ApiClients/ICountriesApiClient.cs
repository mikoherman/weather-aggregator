using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess.ApiClients
{
    public interface ICountriesApiClient
    {
        Task<string> FetchData(Continent continent);
    }
}