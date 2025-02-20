using AutoMapper;
using Nate.AutoMapper;
using Nate.Data.EntityFrameworkCore.Sample.Models.Dtos.Requests;
using Nate.Data.EntityFrameworkCore.Sample.Models.Entities;

namespace Nate.Data.EntityFrameworkCore.Sample.Configurations.AutoMapper.Profiles
{
    public class UserProfile : BaseProfile
    {
        public UserProfile()
        {
            //CreateMap<CreateUserDto, User>();
            //使用 ForMember 显式指定映射关系
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email2));
            CreateMap<UserAddress, CreateUserDto>().ReverseMap();//ReverseMap() 方法来实现双向映射
            //使用自定义解析器
            CreateMap<CreateUserDto, UserAddress>()
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom<AddressResolver>());
        }

        public class AddressResolver : IValueResolver<CreateUserDto, UserAddress, string>
        {
            public string Resolve(CreateUserDto source, UserAddress dest, string destMember, ResolutionContext context)
            {
                return $"{source.Street}, {source.City}, {source.Province}";
            }
        }
    }
}
