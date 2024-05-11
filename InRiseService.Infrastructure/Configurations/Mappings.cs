using AutoMapper;
using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Application.DTOs.UserAddressDto;
using InRiseService.Application.DTOs.UserAutenticationDto;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.DTOs.ZipCodeBaseDto;
using InRiseService.Application.UserDto;
using InRiseService.Domain.Addressed;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.Processors;
using InRiseService.Domain.Users;
using InRiseService.Domain.UsersAddress;

namespace InRiseService.Infrastructure.Configurations
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<User,UserDtoInsertRequest>().ReverseMap();
            CreateMap<User,UserDtoResponse>().ReverseMap();
            CreateMap<User, UserDtoUpdateRequest>().ReverseMap();
            CreateMap<User, UserAutenticationDtoResponse>().ReverseMap();
            CreateMap<UserAddress, UserAddressDtoInsertRequest>().ReverseMap();
            CreateMap<Address, AddressDtoResponse>().ReverseMap();
            CreateMap<Address, UserAddressDtoInsertRequest>().ReverseMap();
            CreateMap<UserAddress, AddressDtoResponse>().ReverseMap();
            CreateMap<Address, UserAddress>().ReverseMap();
            CreateMap<Processor, ProcessorDtoInsertRequest>().ReverseMap();
            CreateMap<MotherBoard, MotherBoardDtoInsertRequest>().ReverseMap();
        }
    }
}