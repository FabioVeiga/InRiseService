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

        public CategoryService(ApplicationContext context,  ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(Category category)
        {
            try
            {
                category.DeleteIn = DateTime.Now;
                await UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(CategoryService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories.Where(c => c.DeleteIn != null && c.Active).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(CategoryService)}::{nameof(GetAllAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            try
            {
                return _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(CategoryService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Category> InsertAsync(string name)
        {
            try
            {
                var model = new Category{
                    Name = name
                };
                _context.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(CategoryService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                category.UpdateIn = DateTime.Now;
                _context.Update(category);
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(CategoryService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}