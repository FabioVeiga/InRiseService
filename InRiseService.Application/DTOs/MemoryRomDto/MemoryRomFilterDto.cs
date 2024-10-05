using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.MemoryRomDto
{
    public class MemoryRomFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? ValueClassification { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}