# Weather Data Aggregator

The **Weather Data Aggregator** is a .NET 8 application designed to fetch, process, and 
display weather data for countries across different continents. It integrates with external APIs, 
caches data for efficiency, and provides functionality to export weather data to files such as CSV.
In this project i have focused on good design principles, clean project structure and use of SOLID principles.


---

## Features

- **Weather Data Retrieval**: Fetches real-time weather data for countries using the OpenWeather API.
- **Country Data Retrieval**: Retrieves country information (e.g., latitude, longitude) using the RestCountries API.
- **Caching**: Implements caching to reduce redundant API calls and improve performance.
- **File Exporting**: Exports weather data to CSV files for easy sharing and analysis.
- **Console Interaction**: Provides a user-friendly console interface for interacting with the application.
- **Extensible Design**: Built using design patterns like the Decorator Pattern for flexibility and maintainability.

---

## Project Structure

### **Key Components**

1. **API Clients**:
   - `OpenWeatherApiClient`: Fetches weather data from the OpenWeather API.
   - `RestCountriesApiClient`: Retrieves country data from the RestCountries API.

2. **Data Providers**:
   - `WeatherDataProvider`: Fetches weather data for multiple countries.
   - `FileExportingWeatherDataProvider`: A decorator that extends `WeatherDataProvider` to export weather data to a file.
   - `CachedCountryDataProvider`: Caches country data to reduce API calls and improve performance.

3. **Models**:
   - `Country`: Represents a country and it's location with properties like `Name`, `Latitude`, and `Longitude`.
   - `WeatherData`: Represents weather information such as temperature, humidity, wind speed, and pressure.

4. **Utilities**:
   - `JsonFileHandler<T>`: Handles reading and writing JSON files.
   - `CsvFileHandler<T>`: Handles exporting data to CSV files.

5. **User Interaction**:
   - `ConsoleUserInteractor`: Handles user input and output in the console.
   - `ConsoleWeatherDataTablePrinter`: Prints weather data in a tabular format in the console.
  
---

## Installation

1. **Clone the Repository**:
   
