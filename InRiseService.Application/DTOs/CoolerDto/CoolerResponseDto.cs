using InRiseService.Application.DTOs.ImageProductDto;

namespace InRiseService.Application.DTOs.CoolerDto
{
    public class CoolerResponseDto
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Air { get; set; } = default!;
        public string Refrigeration { get; set; } = default!;
        public int FanQuantity { get; set; } = default!;
        public int FanDiametric { get; set; } = default!;
        public int MaxVelocit { get; set; } = default!;
        public int Dimension { get; set; } = default!;
        public IList<ImageProductResponseDto> ImageProductResponseDtos { get; set; } = new List<ImageProductResponseDto>();
    }
}