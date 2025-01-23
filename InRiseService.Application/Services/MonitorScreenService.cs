using InRiseService.Application.DTOs.MonitorScreenDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PriceDto;
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
        
        public async Task DeleteAsync(MonitorScreen monitorScreen)
        {
            try
            {
                monitorScreen.DeleteIn = DateTime.Now;
                monitorScreen.Active = false;
                _context.Update(monitorScreen);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MonitorScreenService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<MonitorScreenResponseDto>> GetByFilterAsync(MonitorScreenFilterDto filter)
        {
            try
            {
                var query = _context.MonitorsScreen
                .Include(x => x.Price)
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name)
                );

                if (filter.ValueClassification.HasValue)
                    query = query.Where(x => x.ValueClassification == filter.ValueClassification.Value);

                if (filter.ValueClassification.HasValue)
                    query = query.Where(x => x.ValueClassification == filter.ValueClassification.Value);

                if (filter.IsActive.HasValue)
                    query = query.Where(x => x.Active == filter.IsActive.Value);

                if (filter.IsDeleted.HasValue)
                    query = filter.IsDeleted.Value 
                        ? query.Where(x => x.DeleteIn != null) 
                        : query.Where(x => x.DeleteIn == null);
                
                var listResultDto = query.Select(x => new MonitorScreenResponseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    InsertIn = x.InsertIn,
                    DeleteIn = x.DeleteIn,
                    UpdateIn = x.UpdateIn,
                    Dimesion = x.Dimesion,
                    ValueClassification = x.ValueClassification,
                    Description = x.Description,
                    Quality = x.Quality,
                    UpdateVolume = x.UpdateVolume,
                    Price = new PriceResponseDto(){
                        Id = x.Price!.Id,
                        CostPrice = x.Price.CostPrice,
                        FinalPrice = x.Price.FinalPrice,
                        IVA = x.Price.IVA,
                        PorcentageADMCost = x.Price.PorcentageADMCost,
                        PorcentageDiscount = x.Price.PorcentageDiscount,
                        PorcentageFixedCost = x.Price.PorcentageFixedCost,
                        PorcentageProfit = x.Price.PorcentageProfit,
                        }
                });

                var finalListResult = await listResultDto.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MonitorScreenService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<MonitorScreen?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MonitorsScreen
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MonitorScreenService), nameof(GetByIdAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MonitorScreenService), nameof(InsertAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MonitorScreenService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}