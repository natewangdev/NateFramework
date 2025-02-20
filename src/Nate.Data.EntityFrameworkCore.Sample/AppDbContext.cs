using Microsoft.EntityFrameworkCore;
using Nate.Data.EntityFrameworkCore.Sample.Models.Entities;

namespace Nate.Data.EntityFrameworkCore.Sample
{
    public class AppDbContext : BaseDbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information) // 输出SQL到控制台,只记录Info级别
                .EnableSensitiveDataLogging();// 显示参数值，仅开发模式开启
                                              //.EnableDetailedErrors();// 详细错误信息
        }
    }
}
