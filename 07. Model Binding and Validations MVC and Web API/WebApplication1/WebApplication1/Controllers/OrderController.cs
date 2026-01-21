using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index([FromForm][Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))] Order order)
        {
            if (ModelState.IsValid)
            {
                var rand = new Random();
                return Ok(string.Join("\n", rand.Next(1, 100000)));
            }

            var errorMessages = ModelState.Values.SelectMany(u => u.Errors).Select(u => u.ErrorMessage);
            return BadRequest(string.Join('\n', errorMessages));
        }
    }
}
