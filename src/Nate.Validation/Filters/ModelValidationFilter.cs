using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nate.Core.Models.ApiResult;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Nate.Validation.Filters
{
    public class ModelValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e =>
                    {
                        var propertyName = e.Key;
                        var (displayName, description) = GetDisplayInfo(context.ActionDescriptor.Parameters, propertyName);
                        var errorMessage = e.Value.Errors.First().ErrorMessage;

                        // 使用displayName或propertyName填充错误消息
                        var fieldName = displayName ?? propertyName;
                        errorMessage = string.Format(errorMessage, fieldName);

                        return new
                        {
                            PropertyName = fieldName,
                            Description = description,
                            ErrorMessage = errorMessage
                        };
                    })
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
                    .ToList();

                var result = ApiResult<object>.Fail("One or more validation errors occurred. " + string.Join("; ", errors));
                context.Result = new BadRequestObjectResult(result);
                return;
            }

            await next();
        }

        private (string Name, string Description) GetDisplayInfo(IList<ParameterDescriptor> parameters, string propertyName)
        {
            foreach (var parameter in parameters)
            {
                if (parameter is ControllerParameterDescriptor controllerParameter)
                {
                    var type = controllerParameter.ParameterInfo.ParameterType;
                    var property = type.GetProperty(propertyName);

                    if (property != null)
                    {
                        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                        return displayAttribute != null
                            ? (displayAttribute.Name, displayAttribute.Description)
                            : (null, null);
                    }
                }
            }

            return (null, null);
        }
    }
}
