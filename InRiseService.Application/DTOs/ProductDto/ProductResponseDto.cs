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
        public ProductCategoryResponseSimpleDto Category { get; set; } = default!;
        public string ValueTypeProducts { get; set; } = string.Empty;
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }

    public class ProductCategoryResponseSimpleDto{
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
    }
}