using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Domain.ImagesSite;

namespace InRiseService.Application.Interfaces
{
    public interface IImageService
    {
        Task<ImagensProduct> InsertAsync(ImagensProduct image);
        Task<bool> DeleteAsync(ImagensProduct image);
        Task<ImagensProduct?> GetByIdAsync(int id);
        Task<ICollection<ImageProductResponseDto>> GetByCoolerIdAsync(int idCooler);

        ICollection<ImageCategoryDto> GetImageCategories();
        ImageCategoryDto? GetImageCategoryByName(string nameCategoryImage);
        ImageCategoryDto? GetImageCategoryById(int idCategoryImage);
    }
}