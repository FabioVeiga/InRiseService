using InRiseService.Application.DTOs.TowerDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using InRiseService.Domain.Towers;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.Services
{
    public class TowerService : ITowerService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<TowerService> _logger;

        public TowerService(ApplicationContext context,  ILogger<TowerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(Tower tower)
        {
            try
            {
                tower.DeleteIn = DateTime.Now;
                tower.Active = false;
                _context.Update(tower);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(TowerService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<TowerDtoResponse>> GetByFilterAsync(TowerFilterDto filter)
        {
            try
            {
                var query = _context.Towers
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
                
                var listResultDto = query.Select(x => new TowerDtoResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    InsertIn = x.InsertIn,
                    DeleteIn = x.DeleteIn,
                    UpdateIn = x.UpdateIn,
                    Description = x.Description,
                    ValueClassification = x.ValueClassification,
                    Dimesion = x.Dimesion,
                    MaxFans = x.MaxFans,
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(TowerService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<Tower?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Towers
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(TowerService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<Tower> InsertAsync(Tower Tower)
        {
            try
            {
                _context.Add(Tower);
                await _context.SaveChangesAsync();
                return Tower;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(TowerService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(Tower Tower)
        {
            try
            {
                Tower.UpdateIn = DateTime.Now;
                _context.Update(Tower);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(TowerService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}