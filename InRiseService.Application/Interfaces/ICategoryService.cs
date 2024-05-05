using InRiseService.Domain.Categories;

namespace InRiseService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> InsertAsync(string name);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
    }
}