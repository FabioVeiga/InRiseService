using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.CoolerDto
{
    public class CoolerResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;
        public int ValueClassification { get; set; }
        public string Air { get; set; } = default!;
        public string Refrigeration { get; set; } = default!;
        public int FanQuantity { get; set; } = default!;
        public int FanDiametric { get; set; } = default!;
        public int MaxVelocit { get; set; } = default!;
        public int Dimension { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}