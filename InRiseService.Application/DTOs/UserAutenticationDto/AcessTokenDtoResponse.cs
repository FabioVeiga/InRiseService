namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class AcessTokenDtoResponse
    {
        public string Token { get; private set; } = default!;
        public DateTime ExpiredIn { get; private set; }
    }
}