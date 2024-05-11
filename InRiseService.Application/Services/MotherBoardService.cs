using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.DTOs.PaginationDto;
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

        public async Task<Pagination<MotherBoard>> GetByFilterAsync(MotherBoardDtoFilterRequest filter)
        {
            try
            {
                var query = _context.MotherBoards
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name)
                );

                if(filter.IsDeleted.HasValue)
                    query = query.Where(x => x.DeleteIn != null);
                if(filter.CategoryId.HasValue)
                    query = query.Where(x => x.Category.Id == filter.CategoryId);
                
                var finalListResult = await query.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ProcessorService)}::{nameof(GetByFilterAsync)}] - Exception: {ex}");
                throw;
            }
        }
        
        public async Task DeleteAsync(MotherBoard motherBoard)
        {
            try
            {
                motherBoard.DeleteIn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MotherBoardService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<MotherBoard?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MotherBoards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MotherBoardService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
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
                _logger.LogError($"[{nameof(MotherBoardService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(MotherBoard motherBoard)
        {
            try
            {
                motherBoard.UpdateIn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(MotherBoardService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}