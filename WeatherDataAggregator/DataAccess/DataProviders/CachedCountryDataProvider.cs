using System.Collections.ObjectModel;
using WeatherDataAggregator.DataAccess.DataProviders;
using WeatherDataAggregator.FileHandling;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.DataAccess;

/// <summary>
/// A decorator for <see cref="ICountryDataProvider"/> that adds caching functionality.
/// Implements the Decorator Pattern to enhance the behavior of the wrapped <see cref="ICountryDataProvider"/>.
/// </summary>
/// <remarks>
/// This class caches country data by continent, storing it in memory and persisting it to a file.
/// It delegates calls to the wrapped <see cref="ICountryDataProvider"/> when data is not available in the cache.
/// </remarks>
public class CachedCountryDataProvider :
    ICountryDataProvider, ICache<KeyValuePair<Continent, IEnumerable<Country>>>
{
    private readonly ICountryDataProvider _countryDataProvider;
    private readonly IFileHandler<Dictionary<Continent, IEnumerable<Country>>> _fileHandler;
    private readonly Dictionary<Continent, IEnumerable<Country>> _cachedCountries = new();
    private readonly string _cacheFileName;

    public CachedCountryDataProvider(ICountryDataProvider countryDataProvider, 
        IFileHandler<Dictionary<Continent, IEnumerable<Country>>> fileHandler, string cacheFileName)
    {
        _countryDataProvider = countryDataProvider;
        _fileHandler = fileHandler;
        _cacheFileName = cacheFileName;
        Initialize();
    }
    /// <summary>
    /// Retrieves a list of countries for the specified continent.
    /// </summary>
    /// <param name="continent">The continent for which country data is requested.</param>
    /// <returns>A <see cref="ReadOnlyCollection{T}"/> of <see cref="Country"/> objects for the specified continent.</returns>
    /// <remarks>
    /// If the data is available in the cache, it is returned directly. Otherwise, the method fetches the data
    /// from the wrapped <see cref="ICountryDataProvider"/>, adds it to the cache, and then returns it.
    /// </remarks>
    public async Task<ReadOnlyCollection<Country>> GetCountriesByContinentAsync(Continent continent)
    {
        if (!_cachedCountries.ContainsKey(continent))
        {
            var countries = await _countryDataProvider.GetCountriesByContinentAsync(continent);
            AddToCache(new KeyValuePair<Continent, IEnumerable<Country>>(continent, countries));
            return countries;
        }
        return new ReadOnlyCollection<Country>(_cachedCountries[continent].ToList());
    }
    /// <summary>
    /// Loads cached data from the file into memory.
    /// </summary>
    /// <remarks>
    /// This method reads the cache file using the <see cref="IFileHandler{T}"/> and populates the in-memory cache.
    /// If the file does not exist or contains invalid data, the cache remains empty, and a warning is logged.
    /// </remarks>
    private void Initialize() =>
        (_fileHandler.ReadFromAFile(_cacheFileName)?? new())
        .ToList()
        .ForEach(kvp => _cachedCountries[kvp.Key] = kvp.Value);
    /// <summary>
    /// Adds the specified elements to the cache and persists the updated cache to the file.
    /// </summary>
    /// <param name="elements">An array of key-value pairs where the key is a <see cref="Continent"/> and the value is a collection of <see cref="Country"/> objects.</param>
    /// <remarks>
    /// This method writes the cache to the file using the <see cref="IFileHandler{T}"/>
    /// </remarks>
    public void AddToCache(params KeyValuePair<Continent, IEnumerable<Country>>[] elements)
    {
        _fileHandler.WriteToAFile(_cacheFileName, 
            _cachedCountries.Union(elements).ToDictionary());
    }
}








