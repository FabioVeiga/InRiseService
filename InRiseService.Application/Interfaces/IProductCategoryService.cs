using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.ProductCategoryDto;
using InRiseService.Domain.ProductCategories;

namespace InRiseService.Application.Interfaces
{
    public interface IProductCategoryService
    {
        Task<ProductCategory> InsertAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeleteAsync(ProductCategory category);
        Task<ProductCategory?> GetByIdAsync(int id);
        Task<Pagination<ProductCategoryResponseDto>> GetByFilterAsync(ProductCategoryFilterDto filter);
    }
}