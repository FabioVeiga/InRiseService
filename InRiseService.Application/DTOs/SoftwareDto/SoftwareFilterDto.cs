using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.SoftwareDto
{
    public class SoftwareFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public PaginationRequest Pagination { get; set; } = default!;
    }
}