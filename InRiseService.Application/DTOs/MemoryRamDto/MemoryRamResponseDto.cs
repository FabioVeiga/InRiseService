using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MemoryRamDto
{
    public class MemoryRamResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Socket { get; set; } = default!;
        public double Frequency { get; set; }
        public int Capacity { get; set; }
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}