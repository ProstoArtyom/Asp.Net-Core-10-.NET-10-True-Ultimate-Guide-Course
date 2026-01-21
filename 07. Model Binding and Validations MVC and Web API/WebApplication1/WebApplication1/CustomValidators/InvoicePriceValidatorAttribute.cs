using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebApplication1.Models;

namespace WebApplication1.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo? productsProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (productsProperty != null)
                {
                    var products = (List<Product>)productsProperty.GetValue(validationContext.ObjectInstance)!;

                    var totalPrice = products.Sum(u => u.Price * u.Quantity);
                    var actualPrice = (double)value;

                    if (totalPrice > 0)
                    {
                        if (totalPrice != actualPrice)
                        {
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, totalPrice), new string[] { nameof(validationContext.MemberName) });
                        }
                    }
                    else
                    {
                        return new ValidationResult("No products found to validate invoice price", new string[] { nameof(validationContext.MemberName) });
                    }

                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
