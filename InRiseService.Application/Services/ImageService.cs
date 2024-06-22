using InRiseService.Application.DTOs.ApiSettingDto;
using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
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

        public async Task<ICollection<ImageProductResponseDto>> GetByCoolerIdAsync(int CoolerId)
        {
            try
            {
                var result = _context.ImagensProducts
                .Include(x => x.Cooler)
                .Where(x => x.CoolerId == CoolerId)
                .Select(x => new ImageProductResponseDto(){
                    Id = x.Id,
                    Url = _setting.BaseUrl + "/" + x.Pathkey + "/" + x.ImageName
                });
                return await result.ToListAsync();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ImageService)}::{nameof(GetByCoolerIdAsync)}] - Exception: {ex}");
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
    }
}