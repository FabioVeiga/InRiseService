using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.PowerSupplies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class PowerSuppliesService : IPowerSupplyService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<PowerSuppliesService> _logger;

        public PowerSuppliesService(ApplicationContext context,  ILogger<PowerSuppliesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Pagination<PowerSupply>> GetByFilterAsync(PowerSupplyFilterDto filter)
        {
            try
            {
                var query = _context.PowerSupplies
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
        
        public async Task DeleteAsync(PowerSupply powerSupply)
        {
            try
            {
                powerSupply.DeleteIn = DateTime.Now;
                _context.Update(powerSupply);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(PowerSuppliesService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<PowerSupply?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PowerSupplies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(PowerSuppliesService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<PowerSupply> InsertAsync(PowerSupply PowerSupply)
        {
            try
            {
                _context.Add(PowerSupply);
                await _context.SaveChangesAsync();
                return PowerSupply;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(PowerSuppliesService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(PowerSupply powerSupply)
        {
            try
            {
                powerSupply.UpdateIn = DateTime.Now;
                _context.Update(powerSupply);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(PowerSuppliesService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}