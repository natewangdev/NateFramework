using Nate.Core.Constants;
using Nate.Core.Models.ApiResult;

namespace Nate.Core.Extensions
{
    public static class ApiResultExtensions
    {
        public static ApiResult<T> ToApiResult<T>(this T data) => ApiResult<T>.Ok(data);
        public static ApiResult<T> ToFailResult<T>(this string message, int code = ApiResultCode.Error)
            => ApiResult<T>.Fail(message, code);
    }
}
