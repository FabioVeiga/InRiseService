using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.PowerSupplies;


namespace InRiseService.Application.Interfaces
{
    public interface IPowerSupplyService
    {
        Task<PowerSupply> InsertAsync(PowerSupply PowerSupply);
        Task UpdateAsync(PowerSupply PowerSupply);
        Task DeleteAsync(PowerSupply PowerSupply);
        Task<PowerSupply?> GetByIdAsync(int id);
        Task<Pagination<PowerSupply>> GetByFilterAsync(PowerSupplyFilterDto filter);
    }
}