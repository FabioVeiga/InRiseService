using InRiseService.Application.DTOs.MonitorScreenDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.MonitorsScreen;

namespace InRiseService.Application.Interfaces
{
    public interface IMonitorScreenService
    {
        Task<MonitorScreen> InsertAsync(MonitorScreen MonitorScreen);
        Task UpdateAsync(MonitorScreen MonitorScreen);
        Task DeleteAsync(MonitorScreen MonitorScreen);
        Task<MonitorScreen?> GetByIdAsync(int id);
        Task<Pagination<MonitorScreen>> GetByFilterAsync(MonitorScreenFilterDto filter);
    }
}