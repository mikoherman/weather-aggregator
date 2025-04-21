# Weather Data Aggregator

The Weather Data Aggregator is a .NET 8 application that retrieves, processes, and displays weather data for countries across various continents. 
It integrates with external APIs, implements persistent in-process caching for minimizing API calls, and supports exporting weather data to CSV files.

In this project, I focused on applying design patterns, following SOLID principles, maintaining a clean project structure, and ensuring clear, 
descriptive Git commit messages and branch organization.

## Purpose

The main goal of this project is to demonstrate my view, and understanding of:

- Clean and maintainable project structure
- Object-oriented design principles in C#, including SOLID
- Application of common design patterns, such as the Decorator pattern
- Robust error handling and structured logging
- In-process caching with file persistence to reduce redundant API calls
- Clear separation between external data sources and internal data models, ensuring maintainability and resilience
- Use of well-established libraries like **CsvHelper** and **Serilog** for reliable CSV handling and logging
- Descriptive, atomic Git commits on dedicated feature branches
- Efficient querying using LINQ
- Asynchronous, single-threaded programming for I/O-bound operations like concurrent API requests
- Integration with external RESTful services, including **OpenWeather** and **REST Countries**
- Basic use of Reflection and Attributes
- Handling data in **CSV** and **JSON** formats
- Use of **read-only collections** to enforce immutability and reduce risk of unintended data modification

---

## External Libraries/Services Used

- [CsvHelper](https://joshclose.github.io/CsvHelper/) - For efficient and robust CSV file handling.
- [Serilog](https://serilog.net/) - For structured logging, enabling easier debugging and improving maintainability.
- [RestCountriesAPI](https://restcountries.com/) - For a complete list of countries from provided continent and their location.
- [OpenWeatherAPI](https://openweathermap.org/api) - For real-time weather data based on geographic location.

## Features

- **Weather Data Retrieval**: Fetches real-time weather data for countries using the OpenWeather API.
- **Country Data Retrieval**: Retrieves countries information (name, latitude, longitude) using the RestCountries API.
- **Caching**: Implements caching to reduce redundant API calls.
- **File Exporting**: Exports weather data to CSV files for easy sharing and analysis.
- **Console Interaction**: Provides a user-friendly console interface for interacting with the application.
- **Extensible Design**: Built using design patterns like the Decorator Pattern for flexibility and maintainability.

## How it works
1. Displays a list of available continents for country fetching<br><br>
![countrieslist](https://github.com/user-attachments/assets/e73803b9-b479-488a-9f77-0f79772051d3)
2. Keeps prompting user until a valid input is received (number 1 - 6):<br><br>
![PromptMessage](https://github.com/user-attachments/assets/884aa59d-09fd-4928-b685-43683f528f77)
3. Fetches countries list of a continent from RestCountries API and caches it, or retrieves it from cache if it's available.
4. For each country in the list makes a call to OpenWeather API for the weather data.
5. Displays weather data to the console in a tabular format<br><br>
![ConsoleDisplay](https://github.com/user-attachments/assets/220a833c-ac37-4e58-a6bf-7c3ba8c1fc45)
6. Exports the result to export.csv file.<br><br>
![WeatherDataCsv](https://github.com/user-attachments/assets/b6f365fe-6a62-4876-8e23-dd665c7c59ba)



---

## Project Structure

### **Key Components**

1. **API Clients**:
   - `OpenWeatherApiClient`: Fetches weather data from the OpenWeather API.
   - `RestCountriesApiClient`: Retrieves country data from the RestCountries API.

2. **Data Providers**:
   - `WeatherDataProvider`: Fetches weather data for multiple countries.
   - `FileExportingWeatherDataProvider`: A decorator that adds functionality for `IWeatherDataProvider` to export weather data to a file.
   - `CachedCountryDataProvider`: A decorator that adds functionality for `ICountryDataProvider` for use of caching and reducing API calls.

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

1. **Get an OpenWeather API key**
- Go to https://openweathermap.org/api
- Sign up for a free account
- Navigate to the API keys section in your profile and generate a new key
2. **Set the API key as an environment variable**
- For example, in PowerShell (Windows):
$env:OPEN_WEATHER_API_KEY = "your_api_key_here"
- Or in Bash (Linux/macOS):
export OPEN_WEATHER_API_KEY=your_api_key_here
3. This app reads the key using Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY"). Make sure it's set before running the project.
4. **Clone the Repository**
   
