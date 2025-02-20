namespace Nate.Core.Models.Validation
{
    public class ValidationErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
