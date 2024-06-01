using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.MemoriesRam;

namespace InRiseService.Application.Interfaces
{
    public interface IMemoryRamService
    {
        Task<MemoryRam> InsertAsync(MemoryRam memoryRam);
        Task UpdateAsync(MemoryRam memoryRam);
        Task DeleteAsync(MemoryRam memoryRam);
        Task<MemoryRam?> GetByIdAsync(int id);
        Task<Pagination<MemoryRam>> GetByFilterAsync(MemoryRamFilterDto filter);
    }
}