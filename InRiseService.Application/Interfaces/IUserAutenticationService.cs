using InRiseService.Application.DTOs.UserAutenticationDto;

namespace InRiseService.Application.Interfaces
{
    public interface IUserAutenticationService
    {
        Task<UserAutenticationDtoResponse> AutenticationAsync(UserAutenticationDtoRequest request);
    }
}