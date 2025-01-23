using InRiseService.Application.DTOs.CoolerDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Coolers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class CoolerService : ICoolerService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<CoolerService> _logger;

        public CoolerService(ApplicationContext context,  ILogger<CoolerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(Cooler cooler)
        {
            try
            {
                cooler.DeleteIn = DateTime.Now;
                cooler.Active = false;
                _context.Update(cooler);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CoolerService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<CoolerResponseDto>> GetByFilterAsync(CoolerFilterDto filter)
        {
            try
            {
                var query = _context.Coolers
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

                var listResultDto = query.Select(x => new CoolerResponseDto(){
                    Active = x.Active,
                    Id = x.Id,
                    Air = x.Air,
                    DeleteIn = x.DeleteIn,
                    Description = x.Description,
                    Dimension = x.Dimension,
                    FanDiametric = x.FanDiametric,
                    FanQuantity = x.FanQuantity,
                    InsertIn = x.InsertIn,
                    MaxVelocit = x.MaxVelocit,
                    Name = x.Name,
                    Refrigeration = x.Refrigeration,
                    UpdateIn = x.UpdateIn,
                    ValueClassification = x.ValueClassification,
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CoolerService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<Cooler?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Coolers
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CoolerService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<Cooler> InsertAsync(Cooler cooler)
        {
            try
            {
                _context.Add(cooler);
                await _context.SaveChangesAsync();
                return cooler;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CoolerService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(Cooler cooler)
        {
            try
            {
                cooler.UpdateIn = DateTime.Now;
                _context.Update(cooler);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CoolerService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}