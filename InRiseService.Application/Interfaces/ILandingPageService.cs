using InRiseService.Domain.LandingPages;

namespace InRiseService.Application.Interfaces
{
    public interface ILandingPageService
    {
        Task<LandingPage> InsertAsync(LandingPage model); 
        Task<IList<LandingPage>> GetAll();
    }
}