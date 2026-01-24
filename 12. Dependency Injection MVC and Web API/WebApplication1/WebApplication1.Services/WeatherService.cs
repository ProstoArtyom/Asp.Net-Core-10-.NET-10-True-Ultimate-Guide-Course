using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly List<WeatherData> _weatherDataList = new List<WeatherData>()
        {
            new() { CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
            new() { CityUniqueCode = "NYC", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 3:00"),  TemperatureFahrenheit = 60 },
            new() { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"),  TemperatureFahrenheit = 82 }
        };

        public WeatherService() { }

        public IEnumerable<WeatherData> GetWeatherData()
        {
            return _weatherDataList;
        }

        public WeatherData? GetWeatherData(string cityCode)
        {
            return _weatherDataList.Find(u => u.CityUniqueCode.Equals(cityCode));
        }
    }
}
