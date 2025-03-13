using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.ProductDto;
using InRiseService.Domain.Products;

namespace InRiseService.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<Pagination<ProductResponseDto>> GetByFilterAsync(ProductFilterDto filter);
        Task<Product?> GetByIdAsync(int id);
    }
}