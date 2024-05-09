using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.ProcessorDto
{
    public class ProcessorDtoFilterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Generation { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; } = true;
        public int? CategoryId { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}