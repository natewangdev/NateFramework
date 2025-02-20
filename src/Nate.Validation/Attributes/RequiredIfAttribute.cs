using System.ComponentModel.DataAnnotations;

namespace Nate.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;
        private readonly object _targetValue;

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            _dependentProperty = dependentProperty;
            _targetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (dependentProperty == null)
                return new ValidationResult($"Property {_dependentProperty} not found");

            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance);

            if (Equals(dependentValue, _targetValue) && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
