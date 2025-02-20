using AutoMapper;
using Nate.Data.Core.Models;

namespace Nate.AutoMapper
{
    public abstract class BaseProfile : Profile
    {
        protected BaseProfile()
        {


            // 2. 日期时间处理
            CreateMap<DateTime, string>().ConvertUsing(dt => dt.ToString("yyyy-MM-dd HH:mm:ss"));
            CreateMap<DateTime?, string>().ConvertUsing(dt => Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss"));

            // 3. 枚举处理
            CreateMap<Enum, string>().ConvertUsing(e => e.ToString());

            // 4. 集合类型处理
            CreateMap<List<string>, string>().ConvertUsing(list => string.Join(",", list));

            // 5. 数值类型转换
            CreateMap<decimal, double>().ConvertUsing(d => (double)d);
            CreateMap<decimal, float>().ConvertUsing(d => (float)d);

            // 6. 布尔值处理
            CreateMap<bool, int>().ConvertUsing(b => b ? 1 : 0);
            CreateMap<int, bool>().ConvertUsing(i => i == 1);

            // 7. 字符串处理
            //CreateMap<string, string>().ConvertUsing(str => string.IsNullOrEmpty(str) ? null : str.Trim());
        }

        // 8. 通用的值转换器
        protected void IgnoreNullValues<TSource, TDestination>()
        {
            CreateMap<TSource, TDestination>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcValue) => srcValue != null));
        }

        // 9. 通用的集合映射
        protected void MapList<TSource, TDestination>()
        {
            CreateMap<List<TSource>, List<TDestination>>();
            CreateMap<IEnumerable<TSource>, IEnumerable<TDestination>>();
            CreateMap<ICollection<TSource>, ICollection<TDestination>>();
        }

        // 10. 通用的分页结果映射
        protected void MapPagedList<TSource, TDestination>()
        {
            CreateMap<PagedList<TSource>, PagedList<TDestination>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
