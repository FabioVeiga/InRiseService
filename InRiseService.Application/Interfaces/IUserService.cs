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

    }
}