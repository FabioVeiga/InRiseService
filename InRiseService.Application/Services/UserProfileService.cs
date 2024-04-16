using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(ILogger<UserProfileService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ProfileDto> GetAllProfile()
        {
            try
            {
                var resultListProfileDto = new List<ProfileDto>();
                var enumValues = Enum.GetValues(typeof(EnumProfile));
                foreach (EnumProfile value in enumValues)
                {
                    resultListProfileDto.Add(new ProfileDto(){
                        Id = (int)value,
                        Name = value.ToString()
                    });
                }
                return resultListProfileDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserProfileService)}::{nameof(GetAllProfile)}] - Exception: {ex}");
                throw;
            }
        }

        public ProfileDto? GetById(int id)
        {
            try
            {
                return GetAllProfile().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserProfileService)}::{nameof(GetProfileById)}] - Exception: {ex}");
                throw;
            }
        }

        public ProfileDto? GetProfileById(int id)
        {
            try
            {
                return GetAllProfile().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserProfileService)}::{nameof(GetProfileById)}] - Exception: {ex}");
                throw;
            }
        }

        public ProfileDto? GetProfileByName(string name)
        {
            try
            {
                return GetAllProfile().FirstOrDefault(x => x.Name.ToUpper().Contains(name.ToUpper()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserProfileService)}::{nameof(GetProfileByName)}] - Exception: {ex}");
                throw;
            }
        }
    }
}