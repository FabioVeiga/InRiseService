using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using InRiseService.Application.DTOs.ApiSettingDto;
using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Enums;
using InRiseService.Domain.ImagesSite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InRiseService.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ImageService> _logger;
        private readonly AzureBlobStorageSetting _setting;

        public ImageService(ApplicationContext context,  ILogger<ImageService> logger, IOptions<AzureBlobStorageSetting> options)
        {
            _context = context;
            _logger = logger;
            _setting = options.Value;
        }
        
        public async Task<bool> DeleteAsync(ImagensProduct image)
        {
            try
            {
                _context.Remove(image);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ImageService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        
        public async Task<ImagensProduct?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ImagensProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ImageService)}::{nameof(GetByCoolerIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<ImagensProduct> InsertAsync(ImagensProduct image)
        {
            try
            {
                _context.ImagensProducts.Add(image);
                await _context.SaveChangesAsync();
                return image;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ImageService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    
        public ICollection<ImageCategoryDto> GetImageCategories()
        {
            try
            {
                var resultListProfileDto = new List<ImageCategoryDto>();
                var enumValues = Enum.GetValues(typeof(EnumTypeCategoryImage));
                foreach (EnumTypeCategoryImage value in enumValues)
                {
                    resultListProfileDto.Add(new ImageCategoryDto(){
                        Id = (int)value,
                        Name = value.ToString()
                    });
                }
                return resultListProfileDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ImageService}::{GetImageCategories}] - Exception: {Ex}", nameof(ImageService),nameof(GetImageCategories), ex);
                throw;
            }
        }

        public ImageCategoryDto? GetImageCategoryByName(string nameCategoryImage)
        {
            try
            {
                return GetImageCategories().FirstOrDefault(x => x.Name.Contains(nameCategoryImage, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ImageService}::{GetImageCategoryByName}] - Exception: {Ex}", nameof(ImageService),nameof(GetImageCategoryByName), ex);
                throw;
            }
        }

        public ImageCategoryDto? GetImageCategoryById(int idCategoryImage)
        {
            try
            {
                return GetImageCategories().FirstOrDefault(x => x.Id == idCategoryImage);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ImageService}::{GetImageCategoryByName}] - Exception: {Ex}", nameof(ImageService),nameof(GetImageCategoryByName), ex);
                throw;
            }
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByCoolerIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.Cooler);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByMemoryRamIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.MemoryRam);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByMemoryRomIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.MemoryRom);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByMonitorScreenIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.MonitorScreen);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByMotherBoardIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.MotherBoard);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByComputerIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.Computer);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByPowerSupplyIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.PowerSupply);
        }

        public async Task<ICollection<ImageProductResponseDto>> GetByCategoryIdAsync(int id)
        {
            return await GetImagesByNavigationPropertyAsync(id, x => x.PowerSupply);
        }

        private async Task<ICollection<ImageProductResponseDto>> GetImagesByNavigationPropertyAsync<T>(
            int id,
            Expression<Func<ImagensProduct, T>> navigationProperty)
        {
            try
            {
                var foreignKeyPropertyName = ((MemberExpression)navigationProperty.Body).Member.Name + "Id";
                var result = _context.ImagensProducts
                    .Include(navigationProperty)
                    .AsNoTracking()
                    .Where(x => EF.Property<int>(x, foreignKeyPropertyName) == id)
                    .Select(x => new ImageProductResponseDto
                    {
                        Id = x.Id,
                        Url = _setting.BaseUrl + "/" + x.Pathkey + "/" + x.ImageName
                    });
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ImageService), nameof(GetImagesByNavigationPropertyAsync), ex);
                throw;
            }
        }

    }
}