using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.DTOs.PriceDto;
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

        public async Task<Pagination<PowerSupplyDtoResponse>> GetByFilterAsync(PowerSupplyFilterDto filter)
        {
            try
            {
                var query = _context.PowerSupplies
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
                
                var listResultDto = query.Select(x => new PowerSupplyDtoResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    InsertIn = x.InsertIn,
                    DeleteIn = x.DeleteIn,
                    UpdateIn = x.UpdateIn,
                    Description = x.Description,
                    ValueClassification = x.ValueClassification,
                    Modular = x.Modular,
                    Potency = x.Potency,
                    PotencyReal = x.PotencyReal,
                    Stamp = x.Stamp,
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(PowerSuppliesService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }
        
        public async Task DeleteAsync(PowerSupply powerSupply)
        {
            try
            {
                powerSupply.DeleteIn = DateTime.Now;
                powerSupply.Active = false;
                _context.Update(powerSupply);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(PowerSuppliesService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<PowerSupply?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PowerSupplies
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(PowerSuppliesService), nameof(GetByIdAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(PowerSuppliesService), nameof(GetByIdAsync), ex);
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(PowerSuppliesService), nameof(GetByIdAsync), ex);
                throw;
            }
        }
    }
}