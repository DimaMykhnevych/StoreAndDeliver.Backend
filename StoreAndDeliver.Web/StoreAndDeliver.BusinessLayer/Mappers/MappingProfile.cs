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
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<AddCargoDto, Cargo>().ForMember(c => c.CargoSettings, c => c.MapFrom(s => s.Settings));
            CreateMap<RequestDto, Request>().ReverseMap();
            CreateMap<CargoSettingDto, CargoSetting>().ReverseMap();
            CreateMap<CargoDto, Cargo>().ReverseMap();
            CreateMap<StoreDto, Store>().ReverseMap();
            CreateMap<CargoRequestDto, CargoRequest>().ReverseMap();
            CreateMap<AddStoreDto, Store>().ReverseMap();
            CreateMap<CargoSessionDto, CargoSession>().ReverseMap();
            CreateMap<CarrierDto, Carrier>().ReverseMap();
        }
    }
}
