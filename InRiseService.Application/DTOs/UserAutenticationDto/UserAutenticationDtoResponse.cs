using InRiseService.Application.UserDto;

namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class UserAutenticationDtoResponse : UserDtoResponse
    {
        public UserAuntenticationAcessTokenDtoResponse AcessToken { get; set; } = null!;
    }
}