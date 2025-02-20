using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nate.Data.Core.Models;
using Nate.Data.EntityFrameworkCore.Interfaces;

namespace Nate.Data.EntityFrameworkCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static void ConfigureDbContext<TContext>(IServiceCollection services, DbConfig dbConfig) where TContext : DbContext
        {
            services.AddSingleton(dbConfig);
            services.AddScoped<DbContext, TContext>(sp =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TContext>();

                switch (dbConfig.DbType.ToLower())
                {
                    case "mysql":
                        optionsBuilder.UseMySql(dbConfig.ConnectionString,
                            ServerVersion.AutoDetect(dbConfig.ConnectionString));
                        break;
                    case "postgresql":
                        optionsBuilder.UseNpgsql(dbConfig.ConnectionString);
                        break;
                    default:
                        throw new ArgumentException($"Unsupported database type: {dbConfig.DbType}");
                }

                return (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddNateDbWithEFCore<TContext>(this IServiceCollection services, Func<DbConfig> dbConfigFactory) where TContext : DbContext
        {
            var dbConfig = dbConfigFactory();
            ValidateDbConfig(dbConfig);
            ConfigureDbContext<TContext>(services, dbConfig);
            return services;
        }

        public static IServiceCollection AddNateDbWithEFCore<TContext>(this IServiceCollection services, Action<DbConfig> configureOptions = null) where TContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var dbConfig = configuration.GetSection("DbConfig").Get<DbConfig>();
            configureOptions?.Invoke(dbConfig);

            ValidateDbConfig(dbConfig);
            ConfigureDbContext<TContext>(services, dbConfig);
            return services;
        }

        public static IServiceCollection AddNateDbWithEFCore<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var dbConfig = configuration.GetSection("DbConfig").Get<DbConfig>();
            ValidateDbConfig(dbConfig);
            ConfigureDbContext<TContext>(services, dbConfig);
            return services;
        }

        private static void ValidateDbConfig(DbConfig dbConfig)
        {
            if (dbConfig == null)
            {
                throw new ArgumentNullException(nameof(dbConfig));
            }

            if (string.IsNullOrEmpty(dbConfig.ConnectionString))
            {
                throw new ArgumentException("Connection string cannot be empty", nameof(dbConfig));
            }

            if (string.IsNullOrEmpty(dbConfig.DbType))
            {
                throw new ArgumentException("Database type cannot be empty", nameof(dbConfig));
            }
        }
    }
}
