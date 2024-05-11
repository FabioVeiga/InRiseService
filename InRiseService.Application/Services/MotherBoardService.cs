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