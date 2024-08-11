using InRiseService.Application.DTOs.CoolerDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.Coolers;

namespace InRiseService.Application.Interfaces
{
    public interface ICoolerService
    {
        Task<Cooler> InsertAsync(Cooler Cooler);
        Task UpdateAsync(Cooler Cooler);
        Task DeleteAsync(Cooler Cooler);
        Task<Cooler?> GetByIdAsync(int id);
        Task<Pagination<Cooler>> GetByFilterAsync(CoolerFilterDto filter);
    }
}