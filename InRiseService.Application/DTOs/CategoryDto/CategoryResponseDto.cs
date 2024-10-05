using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.SoftwareDto;

namespace InRiseService.Application.DTOs.CategoryDto
{
    public class CategoryResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;
        public ICollection<SoftwareResponseDto>? Softwares { get; set; }
        public ICollection<ImageProductResponseDto>? Images { get; set; }
    }
}