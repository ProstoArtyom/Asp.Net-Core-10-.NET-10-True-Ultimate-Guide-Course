using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService) 
        { 
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var weatherDataList = _weatherService.GetWeatherData();
            return View(weatherDataList);
        }

        [Route("/weather/{cityCode}")]
        public IActionResult CityWeatherData([FromRoute]string cityCode)
        {
            if (string.IsNullOrWhiteSpace(cityCode))
            {
                return View();
            }

            var weatherData = _weatherService.GetWeatherData(cityCode);
            return View(weatherData);
        }
    }
}
