using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.BusinessLayer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, AppUser>()
                    .ForMember(u => u.Role, m => m.MapFrom(u => u.Role))
                    .ForMember(u => u.UserName, m => m.MapFrom(u => u.UserName));

            CreateMap<EnvironmentSettingDto, EnvironmentSetting>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
