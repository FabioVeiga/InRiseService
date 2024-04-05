using AutoMapper;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Domain.Users;

namespace InRiseService.Infrastructure.Configurations
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<User,UserDtoInsertRequest>();
        }
    }
}