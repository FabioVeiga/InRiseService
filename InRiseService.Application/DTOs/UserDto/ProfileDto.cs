using System.Reflection;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.UserDto
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}