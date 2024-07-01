using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.PowerSupplyDto
{
    public class PowerSupplyDtoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Potency { get; set; }
        public int PotencyReal { get; set; }
        public string Stamp { get; set; } = default!;
        public bool Modular { get; set; }
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}