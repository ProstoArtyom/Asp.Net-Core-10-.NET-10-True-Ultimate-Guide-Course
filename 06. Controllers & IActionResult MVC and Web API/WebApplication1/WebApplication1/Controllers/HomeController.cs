using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank");
        }

        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            return Json(new
            {
                AccountNumber = 1001,
                AccountHolderName = "Example Name",
                CurrentBalance = 5000
            });
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("README.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber?}")]
        public IActionResult GetCurrentBalance()
        {
            if (Request.RouteValues["accountNumber"] == null)
            {
                return NotFound("Account Number should be supplied");
            }

            if (!int.TryParse(Convert.ToString(Request.RouteValues["accountNumber"]), out int accountNumber))
            {
                return BadRequest("The \"accountNumber\" should be an int value");
            }

            if (accountNumber != 1001)
            {
                return BadRequest("Account Number should be 1001");
            }

            return Content("5000");
        }
    }
}
