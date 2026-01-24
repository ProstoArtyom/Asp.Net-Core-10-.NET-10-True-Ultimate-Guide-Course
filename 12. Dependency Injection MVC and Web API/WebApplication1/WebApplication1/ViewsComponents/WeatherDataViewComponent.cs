using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WeatherSolution.ViewComponents
{
    public class WeatherDataViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(WeatherData weatherData)
        {
            ViewBag.WeatherDataCssClass = GetCssClassByFahrenheit(weatherData.TemperatureFahrenheit);
            return View(weatherData);
        }

        private string GetCssClassByFahrenheit(int TemperatureFahrenheit)
        {
            return TemperatureFahrenheit switch
            {
                (< 44) => "blue-back",
                (>= 44) and (< 75) => "green-back",
                (>= 75) => "orange-back"
            };
        }
    }
}