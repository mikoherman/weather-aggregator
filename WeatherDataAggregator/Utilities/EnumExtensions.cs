namespace WeatherDataAggregator.Utilities;

public static class EnumExtensions
{
    /// <summary>
    /// Returns an <see cref="IEnumerable{T}"/> containing all possible values of the specified enum type.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of all values defined in the <typeparamref name="T"/> enum.
    /// </returns>
    public static IEnumerable<T> GetAllValues<T>() where T : Enum =>
        Enum.GetValues(typeof(T)).Cast<T>();
}
