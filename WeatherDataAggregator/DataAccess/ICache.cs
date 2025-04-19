namespace WeatherDataAggregator.DataAccess;
/// <summary>
/// Represents a simple cache for storing and managing small data sets.
/// </summary>
/// <typeparam name="T">The type of elements to be cached.</typeparam>
/// <remarks>
/// This interface is designed for lightweight caching scenarios where the data set is small
/// </remarks>
public interface ICache<T>
{
    /// <summary>
    /// Adds the specified elements to the cache.
    /// </summary>
    /// <param name="elements">The elements to add to the cache.</param>
    /// <remarks>
    /// If an element already exists in the cache, its behavior (e.g., overwrite or ignore) depends on the implementation.
    /// </remarks>
    public void AddToCache(params T[] elements);
}








