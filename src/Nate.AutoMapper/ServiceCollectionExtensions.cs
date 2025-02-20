using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Nate.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNateAutoMapper(this IServiceCollection services)
        {
            // 获取调用者程序集
            var callingAssembly = Assembly.GetCallingAssembly();

            return services.AddNateAutoMapper(callingAssembly);
        }

        public static IServiceCollection AddNateAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            var mappingAssemblies = new List<Assembly>
            {
                typeof(BaseProfile).Assembly  // 基础映射程序集
            };

            if (assemblies?.Any() == true)
            {
                mappingAssemblies.AddRange(assemblies);
            }

            services.AddAutoMapper(mappingAssemblies.ToArray());
            return services;
        }
    }
}
