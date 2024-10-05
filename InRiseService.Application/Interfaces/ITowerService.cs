using InRiseService.Application.DTOs.TowerDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.Towers;

namespace InRiseService.Application.Interfaces
{
    public interface ITowerService
    {
        Task<Tower> InsertAsync(Tower Tower);
        Task UpdateAsync(Tower Tower);
        Task DeleteAsync(Tower Tower);
        Task<Tower?> GetByIdAsync(int id);
        Task<Pagination<Tower>> GetByFilterAsync(TowerFilterDto filter);
    }
}