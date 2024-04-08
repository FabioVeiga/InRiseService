using AutoMapper;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.UserDto;
using InRiseService.Domain.Users;

namespace InRiseService.Infrastructure.Configurations
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<User,UserDtoInsertRequest>().ReverseMap();
            CreateMap<User,UserDtoResponse>().ReverseMap();
            CreateMap<User, UserDtoUpdateRequest>().ReverseMap();
        }
    }
}