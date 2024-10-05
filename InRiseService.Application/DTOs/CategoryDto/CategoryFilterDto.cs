using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.CategoryDto
{
    public class CategoryFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}