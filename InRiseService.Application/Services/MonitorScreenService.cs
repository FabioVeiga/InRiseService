using InRiseService.Application.DTOs.MonitorScreenDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.MonitorsScreen;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class MonitorScreenService : IMonitorScreenService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MonitorScreenService> _logger;

        public MonitorScreenService(ApplicationContext context,  ILogger<MonitorScreenService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(MonitorScreen MonitorScreen)
        {
            try
            {
                MonitorScreen.DeleteIn = DateTime.Now;
                _context.Update(MonitorScreen);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MonitorScreenService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Pagination<MonitorScreen>> GetByFilterAsync(MonitorScreenFilterDto filter)
        {
            try
            {
                var query = _context.MonitorsScreen
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name)
                );

                if(filter.IsDeleted.HasValue)
                    query = query.Where(x => x.DeleteIn != null);
                
                var finalListResult = await query.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ProcessorService)}::{nameof(GetByFilterAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<MonitorScreen?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MonitorsScreen
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MonitorScreenService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<MonitorScreen> InsertAsync(MonitorScreen MonitorScreen)
        {
            try
            {
                _context.Add(MonitorScreen);
                await _context.SaveChangesAsync();
                return MonitorScreen;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MonitorScreenService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(MonitorScreen MonitorScreen)
        {
            try
            {
                MonitorScreen.UpdateIn = DateTime.Now;
                _context.Update(MonitorScreen);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MonitorScreenService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}