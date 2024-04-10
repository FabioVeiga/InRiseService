using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.UserDto
{
    public class UserDtoFilterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public EnumProfile? Profile { get; set; }
        public bool? Marketing { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public PaginationRequest Pagination { get; set; } = default!;
    }
}
