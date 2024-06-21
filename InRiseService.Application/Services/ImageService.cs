using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.ImagesSite;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class ImageService : IImageService
    {

        private readonly ApplicationContext _context;
        private readonly ILogger<ImageService> _logger;

        public ImageService(ApplicationContext context,  ILogger<ImageService> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogError($"[{nameof(ImageService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}