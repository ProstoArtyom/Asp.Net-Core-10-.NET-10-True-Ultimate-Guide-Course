using WebApplication1.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface IWeatherService
    {
        IEnumerable<WeatherData> GetWeatherData();
        WeatherData? GetWeatherData(string cityCode);
    }
}
