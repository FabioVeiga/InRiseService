using InRiseService.Application.DTOs.CategoryDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ApplicationContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task DeleteAsync(Category category)
        {
            try
            {
                category.DeleteIn = DateTime.Now;
                _context.Categories.Update(category);
                await UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Categories
                .Include(x => x.Softwares)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<Category> InsertAsync(Category category)
        {
            try
            {
                category.InsertIn = DateTime.Now;
                category.Active = true;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(Category category)
        {
            try
            {
                category.UpdateIn = DateTime.Now;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(CategoryService), nameof(UpdateAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<Category>> GetByFilterAsync(CategoryFilterDto filter)
        {
            try
            {
                var query = _context.Categories
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name.ToUpper())
                );

                if (filter.IsDeleted.HasValue)
                    if(filter.IsDeleted.Value)
                        query = query.Where(x => x.DeleteIn != null);
                    else
                        query = query.Where(x => x.DeleteIn == null);

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
