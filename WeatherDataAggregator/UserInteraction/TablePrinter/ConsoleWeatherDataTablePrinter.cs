using System.Collections.ObjectModel;
using System.Text;
using WeatherDataAggregator.Models;

namespace WeatherDataAggregator.UserInteraction.TablePrinter;

public class ConsoleWeatherDataTablePrinter : IConsoleWeatherDataTablePrinter
{
    private const int _charactersInCountryNameCell = 30;
    private const int _charactersInCell = 20;
    private const char _columnSeparator = '|';
    private const char _headerRowSeparator = '-';
    private readonly IConsoleUserInteractor _userInteractor;

    public ConsoleWeatherDataTablePrinter(IConsoleUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public void PrintTable(ReadOnlyDictionary<Country, WeatherData> weathersInCountries)
    {
        var sb = new StringBuilder();
        AppendHeaderRow(sb);
        AppendHeaderRowSeparator(sb);
        foreach (var weatherInCountry in weathersInCountries)
        {
            AppendRow(sb, weatherInCountry.Key, weatherInCountry.Value);
        }
        _userInteractor.DisplayMessage(sb.ToString());
    }

    private void AppendHeaderRow(StringBuilder sb)
    {
        sb.Append("Country Name".PadRight(_charactersInCountryNameCell))
          .Append(_columnSeparator);
        var weatherDataProperties = typeof(WeatherData).GetProperties();
        sb.Append(string.Join(_columnSeparator, weatherDataProperties
            .Select(property => $"{property.Name.PadRight(_charactersInCell)}")));
    }
    private void AppendHeaderRowSeparator(StringBuilder sb)
    {
        var weatherDataProperties = typeof(WeatherData).GetProperties();
        int totalCharactersCount = _charactersInCountryNameCell +
          (_charactersInCell + 1) * weatherDataProperties.Count();
        sb.AppendLine()
          .Append(new string(_headerRowSeparator, totalCharactersCount))
          .AppendLine();
    }
    private void AppendRow(StringBuilder sb, Country country, WeatherData weather)
    {
        var countryName = country.Name;
        // check if country name is longer than characters limit in cell
        // if yes assign substring of countryName that is in characters limit
        countryName = countryName.Length > _charactersInCountryNameCell ?
            countryName.Substring(0, _charactersInCountryNameCell) :
            countryName;
        sb.Append(countryName.PadRight(_charactersInCountryNameCell))
            .Append(_columnSeparator);
        var weatherDataProperties = typeof(WeatherData).GetProperties();
        sb.Append(string.Join(_columnSeparator,
            weatherDataProperties.Select(property =>
            $"{property.GetValue(weather)}".PadRight(_charactersInCell))));
        sb.AppendLine();
    }
}
