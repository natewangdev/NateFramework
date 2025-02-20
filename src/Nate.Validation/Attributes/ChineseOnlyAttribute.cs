using System.ComponentModel.DataAnnotations;

namespace Nate.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ChineseOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var str = value.ToString();
            if (!str.All(c => c >= 0x4E00 && c <= 0x9FFF))
            {
                return new ValidationResult(ErrorMessage ?? "只能输入中文字符");
            }

            return ValidationResult.Success;
        }
    }
}
