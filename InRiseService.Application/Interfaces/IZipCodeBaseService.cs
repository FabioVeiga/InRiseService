using InRiseService.Application.DTOs.ZipCodeBaseDto;

namespace InRiseService.Application.Interfaces
{
    public interface IZipCodeBaseService
    {
        Task<ZipCodeBaseDtoResponse?> GetAddressByZipCode(string zipcode);
    }
}