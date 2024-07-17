using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.Computers;

namespace InRiseService.Application.Interfaces
{
    public interface IComputerService
    {
        Task<Computer> InsertAsync(Computer Computer);
        Task UpdateAsync(Computer Computer);
        Task DeleteAsync(Computer Computer);
        Task<Computer?> GetByIdAsync(int id);
    }
}