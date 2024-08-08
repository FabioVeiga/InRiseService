using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.SoftwareDto;
using InRiseService.Domain.Softwares;

namespace InRiseService.Application.Interfaces
{
    public interface ISoftwareService
    {
        Task<Software> InsertAsync(Software software);
        Task DeleteAsync(Software software);
        Task<Software?> GetByIdAsync(int id);
        Task<Pagination<Software>> GetByFilterAsync(SoftwareFilterDto filter);
    }
}