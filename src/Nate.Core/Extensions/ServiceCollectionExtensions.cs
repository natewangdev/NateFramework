using Microsoft.Extensions.DependencyInjection;
using Nate.Core.Services.CurrentUser;

namespace Nate.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNateCore(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
