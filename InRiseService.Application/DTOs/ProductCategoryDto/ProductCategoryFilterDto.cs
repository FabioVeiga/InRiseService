using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.ProductCategoryDto
{
    public class ProductCategoryFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}