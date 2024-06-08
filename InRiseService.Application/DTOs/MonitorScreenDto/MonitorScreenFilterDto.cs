using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.MonitorScreenDto
{
    public class MonitorScreenFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}