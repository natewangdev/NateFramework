using System.ComponentModel.DataAnnotations;

namespace Nate.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly double _minValue;

        public MinValueAttribute(double minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (double.TryParse(value.ToString(), out double numValue))
            {
                if (numValue < _minValue)
                {
                    return new ValidationResult($"Value must be greater than or equal to {_minValue}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
