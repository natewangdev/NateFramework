using Nate.AutoMapper;
using Nate.Core.Extensions;
using Nate.Core.Services.CurrentUser;
using Nate.Data.EntityFrameworkCore.Extensions;
using Nate.Data.EntityFrameworkCore.Sample.Services;
using Nate.Validation.Extensions;

namespace Nate.Data.EntityFrameworkCore.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; //序列化原样输出
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddNateCore();
            builder.Services.AddNateValidation();
            //builder.Services.AddNateDbWithEFCore<AppDbContext>(); //使用默认的appsetting里的DbConfig
            //builder.Services.AddNateDbWithEFCore<AppDbContext>(option =>
            //{
            //    option.DbType = "mysql";
            //    option.ConnectionString = "Server=172.31.116.75;Database=TestDb;User=root;Password=root123;";
            //}); // 手动传入DbConfig

            builder.Services.AddNateDbWithEFCore<AppDbContext>(() => new Core.Models.DbConfig()
            {
                DbType = "postgresql",
                ConnectionString = "Host=172.31.116.75;Database=TestDb;Username=postgres;Password=Password123!;"
            });

            builder.Services.AddNateAutoMapper();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserSampleService>();

            var app = builder.Build();

            app.UseGlobalExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
