using System.ComponentModel.DataAnnotations;
using WebApplication1.CustomValidators;

namespace WebApplication1.Models
{
    public class Order
    {
        [Display(Name = "Order Number")]
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Order Date")]
        [MinimumDateValidator("2000-01-01", ErrorMessage = "Order Date should be greater than or equal to 2000")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Invoice Price")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between a valid number")]
        [InvoicePriceValidator]
        public double InvoicePrice { get; set; }

        [ProductsValidator]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
