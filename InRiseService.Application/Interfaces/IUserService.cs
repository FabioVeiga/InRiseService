using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.UserDto;
using InRiseService.Domain.Users;

namespace InRiseService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> InsertAsync(User user);
        Task<User?> CheckEmailIfExists(string email);
        Task<User?> CheckPhoneNumberIfExists(string phoneNumber);
        Task<User> UpdateAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> DeleteAsync(User user);
        Task<User> ActivateAsync(User user);
        Task<User> DectivateAsync(User user);
        Task<Pagination<UserDtoResponse>> GetUserByFilter(UserDtoFilterRequest request);
    }
}