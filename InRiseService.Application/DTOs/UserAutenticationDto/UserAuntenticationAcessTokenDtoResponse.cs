namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class UserAutenticationAcessTokenDtoResponse
    {
        public string Token { get; private set; } = default!;
        public DateTime ExpiredIn { get; private set; }
        public UserAutenticationAcessTokenDtoResponse(string token, double horas)
        {
            Token = token;
            ExpiredIn = DateTime.Now.AddHours(horas);
        }
    }
}