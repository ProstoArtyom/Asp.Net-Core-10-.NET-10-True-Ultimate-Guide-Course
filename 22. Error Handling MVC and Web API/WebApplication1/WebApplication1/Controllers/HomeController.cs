using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [Route("/Error")]
        public IActionResult Error()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewBag.ErrorMessage = exceptionHandlerFeature != null
                ? exceptionHandlerFeature.Error.Message
                : "Error encountered";

            return View();
        }
    }
}
