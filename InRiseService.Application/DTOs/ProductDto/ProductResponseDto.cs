using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.DTOs.ProductCategoryDto;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.ProductDto
{
    public class ProductResponseDto : BaseDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public ProductCategoryResponseDto Category { get; set; } = default!;
        public IList<EnumValueTypeProduct> ValueTypeProducts { get; set; } = [];
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}