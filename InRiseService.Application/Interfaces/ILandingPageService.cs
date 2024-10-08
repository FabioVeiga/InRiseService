using InRiseService.Domain.LandingPages;

namespace InRiseService.Application.Interfaces
{
    public interface ILandingPageService
    {
        Task<LandingPage> InsertAsync(LandingPage model); 
        Task UpdateAsync(LandingPage model); 
        Task<LandingPage?> GetByEmailAsync(string email);
        Task<IList<LandingPage>> GetAll();
    }
}