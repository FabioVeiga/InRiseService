using InRiseService.Application.DTOs.MemoryRomDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.MemoriesRom;

namespace InRiseService.Application.Interfaces
{
    public interface IMemoryRomService
    {
        Task<MemoryRom> InsertAsync(MemoryRom MemoryRom);
        Task UpdateAsync(MemoryRom MemoryRom);
        Task DeleteAsync(MemoryRom MemoryRom);
        Task<MemoryRom?> GetByIdAsync(int id);
        Task<Pagination<MemoryRom>> GetByFilterAsync(MemoryRomFilterDto filter);
    }
}