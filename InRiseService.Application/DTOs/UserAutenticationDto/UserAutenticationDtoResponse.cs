using InRiseService.Application.UserDto;

namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class UserAutenticationDtoResponse : UserDtoResponse
    {
        public AcessTokenDtoResponse AcessToken { get; set; } = null!;
    }
}