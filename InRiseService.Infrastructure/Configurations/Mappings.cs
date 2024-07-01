using AutoMapper;
using InRiseService.Application.DTOs.CoolerDto;
using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.DTOs.MemoryRomDto;
using InRiseService.Application.DTOs.MonitorScreenDto;
using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Application.DTOs.TowerDto;
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
using InRiseService.Domain.MonitorsScreen;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.PowerSupplies;
using InRiseService.Domain.Prices;
using InRiseService.Domain.Processors;
using InRiseService.Domain.Towers;
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

            CreateMap<MemoryRam, MemoryRamInsertDto>().ReverseMap();
            CreateMap<MemoryRam,MemoryRamResponseDto>().ReverseMap();
            CreateMap<MemoryRamInsertDto,MemoryRam>().ForMember(dest => dest.Price, opt => opt.Ignore());
            CreateMap<MemoryRamResponseDto,MemoryRam>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<MemoryRom, MemoryRomRequestDto>().ReverseMap();
            CreateMap<MemoryRom,MemoryRomResponseDto>().ReverseMap();
            CreateMap<MemoryRomResponseDto,MemoryRom>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<MonitorScreen, MonitorScreenRequestDto>().ReverseMap();
            CreateMap<MonitorScreen,MonitorScreenResponseDto>().ReverseMap();
            CreateMap<MonitorScreenResponseDto,MonitorScreen>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<MotherBoard, MotherBoardDtoRequest>().ReverseMap();
            CreateMap<MotherBoard,MotherBoardDtoResponse>().ReverseMap();
            CreateMap<MotherBoardDtoResponse,MotherBoard>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<PowerSupply, PowerSupplyDtoRequest>().ReverseMap();
            CreateMap<PowerSupply, PowerSupplyDtoResponse>().ReverseMap();
            CreateMap<PowerSupplyDtoResponse,PowerSupply>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<Processor, ProcessorDtoRequest>().ReverseMap();
            CreateMap<Processor, ProcessorDtoResponse>().ReverseMap();
            CreateMap<ProcessorDtoResponse,Processor>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<Tower, TowerDtoRequest>().ReverseMap();
            CreateMap<Tower, TowerDtoResponse>().ReverseMap();
            CreateMap<TowerDtoResponse,Tower>().ForMember(dest => dest.Price, opt => opt.Ignore());

            CreateMap<VideoBoard, VideoBoardDtoResquest>().ReverseMap();
            CreateMap<VideoBoard, VideoBoardDtoResponse>().ReverseMap();
            CreateMap<VideoBoardDtoResponse,VideoBoard>().ForMember(dest => dest.Price, opt => opt.Ignore());
            
            CreateMap<CoolerInsertDto,Cooler>().ForMember(dest => dest.Price, opt => opt.Ignore());
            CreateMap<Cooler, CoolerInsertDto>().ReverseMap();
            CreateMap<Cooler,CoolerResponseDto>().ForMember(dest => dest.Price, opt => opt.Ignore());
            CreateMap<CoolerResponseDto, Cooler>().ForMember(dest => dest.Price, opt => opt.Ignore());
            
            CreateMap<Price, PriceRequestDto>().ReverseMap();
            CreateMap<Price, PriceResponseDto>().ReverseMap();
        }
    }
}