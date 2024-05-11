
using InRiseService.Application.DTOs.PaginationDto;

namespace InRiseService.Application.DTOs.MotherBoardDto
{
    public class MotherBoardDtoFilterRequest
    {
        public string Name { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public int? CategoryId { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}