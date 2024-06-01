using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.PowerSupplyDto
{
    public class PowerSupplyFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}