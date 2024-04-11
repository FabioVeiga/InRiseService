
using InRiseService.Domain.Addressed;

namespace InRiseService.Application.Interfaces
{
    public interface IAddressService
    {
        Task<Address> InsertAsync(Address address);
        Task<Address?> GetByPostalCode(string postalCode);
    }
}