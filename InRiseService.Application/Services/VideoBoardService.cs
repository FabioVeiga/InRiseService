using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.VideoBoardDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.VideoBoards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class VideoBoardService : IVideoBoardService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<VideoBoardService> _logger;

        public VideoBoardService(ApplicationContext context,  ILogger<VideoBoardService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task DeleteAsync(VideoBoard videoBoard)
        {
            try
            {
                videoBoard.DeleteIn = DateTime.Now;
                videoBoard.Active = false;
                _context.Update(videoBoard);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(VideoBoardService), nameof(DeleteAsync), ex);
                throw;
            }
        }

         public async Task<Pagination<VideoBoard>> GetByFilterAsync(VideoBoardFilterDto filter)
        {
            try
            {
                var query = _context.VideosBoard
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
                
                var finalListResult = await query.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(VideoBoardService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<VideoBoard?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.VideosBoard
                .Include(x => x.Price)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(VideoBoardService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<VideoBoard> InsertAsync(VideoBoard videoBoard)
        {
            try
            {
                _context.Add(videoBoard);
                await _context.SaveChangesAsync();
                return videoBoard;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(VideoBoardService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(VideoBoard videoBoard)
        {
            try
            {
                videoBoard.UpdateIn = DateTime.Now;
                _context.Update(videoBoard);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(VideoBoardService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}