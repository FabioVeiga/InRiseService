using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.SoftwareDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Softwares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<SoftwareService> _logger;

        public SoftwareService(ApplicationContext context,  ILogger<SoftwareService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(Software software)
        {
            try
            {
                software.DeleteIn = DateTime.Now;
                _context.Softwares.Remove(software);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Software?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Softwares
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<Software> InsertAsync(Software software)
        {
            try
            {
                software.InsertIn = DateTime.Now;
                _context.Add(software);
                await _context.SaveChangesAsync();
                return software;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<Software>> GetByFilterAsync(SoftwareFilterDto filter)
        {
            try
            {
                var query = _context.Softwares
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name.ToUpper())
                );

                var finalListResult = await query.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }
    }
}