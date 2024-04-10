namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class UserAuntenticationAcessTokenDtoResponse
    {
        public string Token { get; private set; } = default!;
        public DateTime ExpiredIn { get; private set; }
    }
}