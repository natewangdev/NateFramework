using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nate.Core.Constants;
using Nate.Core.Exceptions;
using Nate.Core.Models.ApiResult;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.Json;

namespace Nate.Core.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var result = exception switch
            {
                BusinessException ex => HandleBusinessException(ex),
                ValidationException ex => HandleValidationException(ex),
                SecurityException ex => HandleSecurityException(ex),
                UnauthorizedAccessException ex => HandleUnauthorizedException(ex),
                ArgumentException ex => HandleArgumentException(ex),
                HttpRequestException ex => HandleHttpRequestException(ex),
                _ => HandleUnknownException(exception)
            };

            // 在开发环境下添加详细错误信息
            if (_environment.IsDevelopment())
            {
                var debugInfo = new
                {
                    ExceptionType = exception.GetType().Name,
                    ExceptionMessage = exception.Message,
                    exception.StackTrace,
                    InnerException = exception.InnerException?.Message,
                    exception.Source
                };
                result.Data = debugInfo;
            }

            context.Response.StatusCode = result.Code;
            var jsonString = JsonSerializer.Serialize(result);
            await context.Response.WriteAsync(jsonString);
        }

        private ApiResult<object> HandleBusinessException(BusinessException exception)
        {
            _logger.LogWarning(exception, exception.Message);
            return ApiResult<object>.Fail(exception.Message, exception.Code);
        }

        private ApiResult<object> HandleValidationException(ValidationException exception)
        {
            _logger.LogWarning(exception, "数据验证失败");
            return ApiResult<object>.Fail(exception.Message, (int)HttpStatusCode.BadRequest);
        }

        private ApiResult<object> HandleSecurityException(SecurityException exception)
        {
            _logger.LogError(exception, "安全验证失败");
            return ApiResult<object>.Fail("安全验证失败", (int)HttpStatusCode.Forbidden);
        }

        private ApiResult<object> HandleUnauthorizedException(UnauthorizedAccessException exception)
        {
            _logger.LogWarning(exception, "未授权访问");
            return ApiResult<object>.Fail("未授权访问", (int)HttpStatusCode.Unauthorized);
        }

        private ApiResult<object> HandleArgumentException(ArgumentException exception)
        {
            _logger.LogWarning(exception, "参数错误");
            return ApiResult<object>.Fail(exception.Message, (int)HttpStatusCode.BadRequest);
        }

        private ApiResult<object> HandleHttpRequestException(HttpRequestException exception)
        {
            _logger.LogError(exception, "HTTP请求错误");
            var statusCode = exception.StatusCode ?? HttpStatusCode.BadRequest;
            return ApiResult<object>.Fail($"HTTP请求错误: {exception.Message}", (int)statusCode);
        }

        private ApiResult<object> HandleUnknownException(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return ApiResult<object>.Fail("服务器内部错误", ApiResultCode.ServerError);
        }
    }
}
