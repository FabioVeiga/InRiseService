using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.MotherBoards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class MotherBoardService : IMotherBoardService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MotherBoardService> _logger;

        public MotherBoardService(ApplicationContext context,  ILogger<MotherBoardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Pagination<MotherBoardDtoResponse>> GetByFilterAsync(MotherBoardDtoFilterRequest filter)
        {
            try
            {
                var query = _context.MotherBoards
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
                
                var listResultDto = query.Select(x => new MotherBoardDtoResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    InsertIn = x.InsertIn,
                    DeleteIn = x.DeleteIn,
                    UpdateIn = x.UpdateIn,
                    Description = x.Description,
                    ValueClassification = x.ValueClassification,
                    Socket = x.Socket,
                    SocketM2 = x.SocketM2,
                    SocketMemory = x.SocketMemory,
                    SocketMemoryVideo = x.SocketMemoryVideo,
                    SocketSSD = x.SocketSSD,
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
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MotherBoardService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }
        
        public async Task DeleteAsync(MotherBoard motherBoard)
        {
            try
            {
                motherBoard.DeleteIn = DateTime.Now;
                motherBoard.Active = false;
                _context.Update(motherBoard);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MotherBoardService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<MotherBoard?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MotherBoards
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MotherBoardService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<MotherBoard> InsertAsync(MotherBoard motherBoard)
        {
            try
            {
                _context.Add(motherBoard);
                await _context.SaveChangesAsync();
                return motherBoard;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MotherBoardService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(MotherBoard motherBoard)
        {
            try
            {
                motherBoard.UpdateIn = DateTime.Now;
                _context.Update(motherBoard);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(MotherBoardService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}