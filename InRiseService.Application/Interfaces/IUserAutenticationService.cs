using InRiseService.Application.DTOs.UserAutenticationDto;

namespace InRiseService.Application.Interfaces
{
    public interface IUserAutenticationService
    {
        UserAutenticationAcessTokenDtoResponse GetTokenAsync(UserAutenticationDtoRequest request);
    }
}