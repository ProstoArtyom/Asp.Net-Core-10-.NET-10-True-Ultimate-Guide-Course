using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApplication1.ServiceContracts.DTOs.ValidationAttributes
{
    public class MinDateAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;
        public MinDateAttribute(int minYear, int minMonth, int minDay)
        {
            _minDate = new DateTime(minYear, minMonth, minDay);
        }

        public MinDateAttribute(string minDate)
        {
            _minDate = DateTime.Parse(minDate, CultureInfo.InvariantCulture);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime < _minDate)
                    return new ValidationResult($"Date cannot be older than {_minDate:MMM dd, yyyy}");
            }
            return ValidationResult.Success;
        }
    }
}
