using InRiseService.Domain.UsersAddress;

namespace InRiseService.Application.Interfaces
{
    public interface IUserAddressService
    {
        Task<UserAddress> InsertAsync(UserAddress userAddress);
        Task<UserAddress?> GetCurrentDefaultAsync(int userId);
        Task<UserAddress> UpdateAsync(UserAddress address);
        Task<UserAddress?> GetByIdAsync(int id);
        Task <IEnumerable<UserAddress>> GetByUserIdAsync(int userId);
        Task RemoveAsync(UserAddress address);
    }
}