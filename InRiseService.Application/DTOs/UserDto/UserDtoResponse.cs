using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.UserDto
{
    public class UserDtoResponse
    {
        public string Name { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool EmailValide { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public bool PhoneNumberValide { get; set; }
        public bool Marketing { get; set; }
        public bool Term { get; set; }
    }
}