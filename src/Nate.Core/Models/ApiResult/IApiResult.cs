namespace Nate.Core.Models.ApiResult
{
    public interface IApiResult
    {
        int Code { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
    }
}
