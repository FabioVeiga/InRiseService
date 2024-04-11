using InRiseService.Application.DTOs.ZipCodeBaseDto;

namespace InRiseService.Application.Interfaces
{
    public interface IZipCodeBaseService
    {
        Task<AddressDtoResponse?> GetAddressByZipCode(string zipcode);
    }
}