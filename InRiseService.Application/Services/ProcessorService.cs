using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Processors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class ProcessorService : IProcessorService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProcessorService> _logger;

        public ProcessorService(ApplicationContext context,  ILogger<ProcessorService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(Processor processor)
        {
            try
            {
                processor.DeleteIn = DateTime.Now;
                await UpdateAsync(processor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ProcessorService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Pagination<Processor>> GetByFilterAsync(ProcessorDtoFilterRequest filter)
        {
            try
            {
                var query = _context.Processors
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

        public async Task<Processor?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Processors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ProcessorService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Processor> InsertAsync(Processor processor)
        {
            try
            {
                processor.InsertIn = DateTime.Now;
                processor.Active = true;
                _context.Add(processor);
                await _context.SaveChangesAsync();
                return processor;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ProcessorService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(Processor processor)
        {
            try
            {
                processor.UpdateIn = DateTime.Now;
                _context.Processors.Update(processor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ProcessorService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}