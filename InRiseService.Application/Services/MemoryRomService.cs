using InRiseService.Application.DTOs.MemoryRomDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.MemoriesRom;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class MemoryRomService : IMemoryRomService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MemoryRomService> _logger;

        public MemoryRomService(ApplicationContext context,  ILogger<MemoryRomService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(MemoryRom memoryRom)
        {
            try
            {
                memoryRom.DeleteIn = DateTime.Now;
                _context.Update(memoryRom);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRomService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Pagination<MemoryRom>> GetByFilterAsync(MemoryRomFilterDto filter)
        {
            try
            {
                var query = _context.MemoriesRom
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

        public async Task<MemoryRom?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MemoriesRom
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRomService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<MemoryRom> InsertAsync(MemoryRom MemoryRom)
        {
            try
            {
                _context.Add(MemoryRom);
                await _context.SaveChangesAsync();
                return MemoryRom;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRomService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(MemoryRom memoryRom)
        {
            try
            {
                memoryRom.UpdateIn = DateTime.Now;
                _context.Update(memoryRom);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MemoryRomService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
        
    }
}