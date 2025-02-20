using Nate.Core.Constants;

namespace Nate.Core.Models.ApiResult
{
    public class ApiResult<T> : IApiResult
    {
        public int Code { get; set; } = ApiResultCode.Success;
        public string Message { get; set; } = "success";
        public T Data { get; set; }
        public bool Success { get; set; } = true;

        public static ApiResult<T> Ok(T data) => new() { Data = data };
        public static ApiResult<T> Fail(string message, int code = ApiResultCode.Error)
            => new() { Message = message, Code = code, Success = false };
    }
}
