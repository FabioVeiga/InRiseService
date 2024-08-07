using InRiseService.Application.DTOs.CategoryDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.Categories;

namespace InRiseService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> InsertAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<Category?> GetByIdAsync(int id);
        Task<Pagination<Category>> GetByFilterAsync(CategoryFilterDto filter);
    }
}
