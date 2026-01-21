using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.CustomValidators
{
    public class ProductsValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Order should have at least one product";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var products = (List<Product>)value;
                if (products.Count == 0)
                {
                    return new ValidationResult(ErrorMessage ?? DefaultErrorMessage, new string[] { nameof(validationContext.MemberName) });
                }

                return ValidationResult.Success;
            }

            return null;
        }
    }
}
