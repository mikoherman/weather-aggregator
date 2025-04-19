using System.Collections.ObjectModel;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess.DataProviders;

public interface ICountryDataProvider
{
    Task<ReadOnlyCollection<Country>> GetCountriesByContinentAsync(Continent continent);
}