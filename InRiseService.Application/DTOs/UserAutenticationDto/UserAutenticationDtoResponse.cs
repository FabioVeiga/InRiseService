using InRiseService.Application.UserDto;

namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class UserAutenticationDtoResponse : UserDtoResponse
    {
        public UserAutenticationAcessTokenDtoResponse AcessToken { get; set; } = null!;
    }
}