using InRiseService.Application.DTOs.UserDto;

namespace InRiseService.Application.Interfaces
{
    public interface IUserProfileService
    {
        public IEnumerable<ProfileDto> GetAllProfile();
        public ProfileDto? GetProfileById(int id);
        public ProfileDto? GetProfileByName(string name);
    }
}