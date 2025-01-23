using InRiseService.Application.DTOs.MemoryRomDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PriceDto;
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
                memoryRom.Active = false;
                _context.Update(memoryRom);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRomService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<MemoryRomResponseDto>> GetByFilterAsync(MemoryRomFilterDto filter)
        {
            try
            {
                var query = _context.MemoriesRom
                .Include(x => x.Price)
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
                
                var listResultDto = query.Select(x => new MemoryRomResponseDto()
                {
                    Capacity = x.Capacity,
                    Id = x.Id,
                    Name = x.Name,
                    ValueClassification = x.ValueClassification,
                    Active = x.Active,
                    DeleteIn = x.DeleteIn,
                    InsertIn = x.InsertIn,
                    IsHHD = x.IsHHD,
                    IsSSD = x.IsSSD,
                    IsSSDM2 = x.IsSSDM2,
                    Potency = x.Potency,
                    Socket = x.Socket,
                    UpdateIn = x.UpdateIn,
                    VelocityRead = x.VelocityRead,
                    VelocityWrite = x.VelocityWrite,
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRomService), nameof(GetByFilterAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRomService), nameof(GetByIdAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRomService), nameof(InsertAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MemoryRomService), nameof(UpdateAsync), ex);
                throw;
            }
        }
        
    }
}