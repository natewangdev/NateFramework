using Microsoft.AspNetCore.Builder;

namespace Nate.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModelValidation(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
