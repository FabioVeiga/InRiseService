using InRiseService.Domain.UsersAddress;

namespace InRiseService.Application.Interfaces
{
    public interface IUserAddressService
    {
        Task<UserAddress> InsertAsync(UserAddress userAddress);
    }
}