using InRiseService.Application.DTOs.ComputerDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Computers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class ComputerService : IComputerService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ComputerService> _logger;

        public ComputerService(ApplicationContext context,  ILogger<ComputerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(Computer computer)
        {
            try
            {
                computer.DeleteIn = DateTime.Now;
                _context.Update(computer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ComputerService}::{DeleteAsync}] - Exception: {Ex}", nameof(ComputerService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<Computer>> GetByFilterAsync(ComputerFilterDto filter)
        {
            try
            {
                var query = _context.Computers
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

        public async Task<Computer?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Computers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ComputerService}::{GetByIdAsync}] - Exception: {Ex}", nameof(ComputerService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<Computer> InsertAsync(Computer computer)
        {
            try
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();
                return computer;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ComputerService}::{InsertAsync}] - Exception: {Ex}", nameof(ComputerService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(Computer computer)
        {
            try
            {
                computer.UpdateIn = DateTime.Now;
                _context.Update(computer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ComputerService}::{UpdateAsync}] - Exception: {Ex}", nameof(ComputerService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}