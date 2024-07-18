using InRiseService.Application.DTOs.ImageProductDto;

namespace InRiseService.Application.DTOs.ComputerDto
{
    public class ComputerResponseDto
    {
        public string Name { get; set; } = default!;
        public int ProcessadorId { get; set; }
        public int MotherBoardId { get; set; }
        public int TowerId { get; set; }
        public int MemoryRamSlot01Id { get; set; }
        public int MemoryRamSlot02Id { get; set; }
        public int MemoryRomHHDId { get; set; }
        public int MemoryRomSSDId { get; set; }
        public int MemoryRomSSDM2Id { get; set; }
        public int VideoBoardId { get; set; }
        public int PowerSupplyId { get; set; }
        public int CoolerId { get; set; }
        public int MonitorScreenId { get; set; }
        public decimal FinalPrice { get; set; }
        public ICollection<ImageProductResponseDto>? Images { get; set; }
    }
}