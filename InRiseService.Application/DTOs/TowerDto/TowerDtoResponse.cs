using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.TowerDto
{
    public class TowerDtoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Dimesion { get; set; } = default!;
        public int MaxFans { get; set; } = default!;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}