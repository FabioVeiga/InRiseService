using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.VideoBoardDto
{
    public class VideoBoardDtoResponse
    {
        public string Name { get; set; } = default!;
        public string Socket { get; set; } = default!;
        public int Bits { get; set; } = default!;
        public int Capacity { get; set; } = default!;
        public string Dimension { get; set; } = default!;
        public int Potency { get; set; } = default!;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}