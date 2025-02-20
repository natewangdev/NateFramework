using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nate.Validation.Filters;

namespace Nate.Validation.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddNateValidation(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ModelValidationFilter>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; //自带的参数校验,true:禁用，false:启用
            });

            return services;
        }
    }
}
