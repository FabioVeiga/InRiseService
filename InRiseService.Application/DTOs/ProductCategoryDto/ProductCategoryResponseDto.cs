using InRiseService.Application.DTOs.ProductDto;

namespace InRiseService.Application.DTOs.ProductCategoryDto
{
    public class ProductCategoryResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = string.Empty;

        public ICollection<ProductResponseDto>? Products { get; set; }
    }
}