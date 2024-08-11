using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.VideoBoardDto
{
    public class VideoBoardFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? ValueClassification { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}