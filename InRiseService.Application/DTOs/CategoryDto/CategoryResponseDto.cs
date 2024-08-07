using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.CategoryDto
{
    public class CategoryResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
    }
}