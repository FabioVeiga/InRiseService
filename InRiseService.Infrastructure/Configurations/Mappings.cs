using AutoMapper;
using InRiseService.Application.DTOs.CoolerDto;
using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.DTOs.MemoryRomDto;
using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Application.DTOs.UserAddressDto;
using InRiseService.Application.DTOs.UserAutenticationDto;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.DTOs.VideoBoardDto;
using InRiseService.Application.DTOs.ZipCodeBaseDto;
using InRiseService.Application.UserDto;
using InRiseService.Domain.Addressed;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.MemoriesRam;
using InRiseService.Domain.MemoriesRom;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.PowerSupplies;
using InRiseService.Domain.Processors;
using InRiseService.Domain.Users;
using InRiseService.Domain.UsersAddress;
using InRiseService.Domain.VideoBoards;

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
            CreateMap<MemoryRam, MemoryRamInsertDto>().ReverseMap();
            CreateMap<MemoryRom, MemoryRomInsertDto>().ReverseMap();
            CreateMap<VideoBoard, VideoBoardInsertDto>().ReverseMap();
            CreateMap<PowerSupply, PowerSupplyInsertDto>().ReverseMap();
            CreateMap<Cooler, CoolerInsertDto>().ReverseMap();
            CreateMap<Cooler, CoolerResponseDto>().ReverseMap();
        }
    }
}