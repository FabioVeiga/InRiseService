using InRiseService.Application.DTOs.ImageProductDto;

namespace InRiseService.Application.DTOs.SoftwareDto
{
    public class SoftwareResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;
        public int ProcessadorMinId { get; set; }
        public int ProcessadorIdealId { get; set; }
        public int MemoryRamMinId { get; set; }
        public int MemoryRamIdealId { get; set; }
        public int VideoBoardMinId { get; set; }
        public int VideoBoardIdealId { get; set; }
        public ICollection<ImageProductResponseDto>? Images { get; set; }
    }
}