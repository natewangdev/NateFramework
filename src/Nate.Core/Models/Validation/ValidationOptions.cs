namespace Nate.Core.Models.Validation
{
    public class ValidationOptions
    {
        public int ErrorCode { get; set; } = 400;
        public string ErrorMessage { get; set; } = "参数验证失败";
        public bool IncludeExceptionDetails { get; set; }
    }
}
