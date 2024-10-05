using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.MemoriesRam;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class MemoryRamService : IMemoryRamService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MemoryRamService> _logger;

        public MemoryRamService(ApplicationContext context,  ILogger<MemoryRamService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(MemoryRam memoryRam)
        {
            try
            {
                memoryRam.DeleteIn = DateTime.Now;
                memoryRam.Active = false;
                _context.Update(memoryRam);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRamService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<MemoryRam>> GetByFilterAsync(MemoryRamFilterDto filter)
        {
            try
            {
                var query = _context.MemoriesRam
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name.ToUpper()));

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
                                    
                var finalListResult = await query.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRamService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<MemoryRam?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MemoriesRam
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRamService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<MemoryRam> InsertAsync(MemoryRam memoryRam)
        {
            try
            {
                _context.Add(memoryRam);
                await _context.SaveChangesAsync();
                return memoryRam;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRamService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(MemoryRam memoryRam)
        {
            try
            {
                memoryRam.UpdateIn = DateTime.Now;
                _context.Update(memoryRam);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRamService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}