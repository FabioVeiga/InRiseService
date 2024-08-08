using InRiseService.Application.DTOs.ImageProductDto;

namespace InRiseService.Application.DTOs.SoftwareDto
{
    public class SoftwareDtoResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
    }
}