using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Domain.ImagesSite;

namespace InRiseService.Application.Interfaces
{
    public interface IImageService
    {
        Task<ImagensProduct> InsertAsync(ImagensProduct image);
        Task<bool> DeleteAsync(ImagensProduct image);
        Task<ImagensProduct?> GetByIdAsync(int id);

        Task<ICollection<ImageProductResponseDto>> GetByCoolerIdAsync(int id);
        Task<ICollection<ImageProductResponseDto>> GetByMemoryRamIdAsync(int id);
        Task<ICollection<ImageProductResponseDto>> GetByMemoryRomIdAsync(int id);
        Task<ICollection<ImageProductResponseDto>> GetByMonitorScreenIdAsync(int id);
        Task<ICollection<ImageProductResponseDto>> GetByMotherBoardIdAsync(int id);
        Task<ICollection<ImageProductResponseDto>> GetByPowerSupplyIdAsync(int id);
        

        ICollection<ImageCategoryDto> GetImageCategories();
        ImageCategoryDto? GetImageCategoryByName(string nameCategoryImage);
        ImageCategoryDto? GetImageCategoryById(int idCategoryImage);
    }
}