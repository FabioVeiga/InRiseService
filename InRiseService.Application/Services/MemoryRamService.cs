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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRamService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Pagination<MemoryRam>> GetByFilterAsync(MemoryRamFilterDto filter)
        {
            try
            {
                var query = _context.MemoriesRam
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

        public async Task<MemoryRam?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MemoriesRam
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRamService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
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
                _logger.LogError($"[{nameof(MemoryRamService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(MemoryRam memoryRam)
        {
            try
            {
                memoryRam.UpdateIn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRamService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}