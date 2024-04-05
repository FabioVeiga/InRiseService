using InRiseService.Domain.Users;

namespace InRiseService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> InsertAsync(User user);
    }
}