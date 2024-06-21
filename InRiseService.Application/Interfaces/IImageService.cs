using InRiseService.Domain.ImagesSite;

namespace InRiseService.Application.Interfaces
{
    public interface IImageService
    {
        Task<ImagensProduct> InsertAsync(ImagensProduct image);
        Task<bool> DeleteAsync(ImagensProduct image);
    }
}