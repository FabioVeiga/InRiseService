using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.ProcessorDto
{
    public class ProcessorDtoResponse
    {
        public string Name { get; set; } = default!;
        public string Generation { get; set; } = default!;
        public string Socket { get; set; } = default!;
        public int Core { get; set; }
        public double Frequency { get; set; }
        public int Potency { get; set; }
        public string SuportMemoryRAM { get; set; } = default!;
        public string SuportMemoryROM { get; set; } = default!;
        public string SuportVideo { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}