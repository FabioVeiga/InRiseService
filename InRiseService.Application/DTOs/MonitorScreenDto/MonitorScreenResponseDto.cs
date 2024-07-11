using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MonitorScreenDto
{
    public class MonitorScreenResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Dimesion { get; set; } = default!;
        public int UpdateVolume { get; set; } = default!;
        public string Quality { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}