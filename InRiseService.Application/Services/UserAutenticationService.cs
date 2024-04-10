using System.Text;
using InRiseService.Application.DTOs.UserAutenticationDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class UserAutenticationService : IUserAutenticationService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UserAutenticationService> _logger;

        public UserAutenticationService(ApplicationContext context, ILogger<UserAutenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<UserAutenticationDtoResponse> AutenticationAsync(UserAutenticationDtoRequest request)
        {
            throw new NotImplementedException();
        }

        /* private UserAuntenticationAcessTokenDtoResponse GetAcessToken(UserAutenticationDtoRequest request)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(request.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Apelido),
                        new Claim(ClaimTypes.Role, usuario.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(horas),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
            }
            catch (Exception)
            {
                _logger.LogError($"[{nameof(UserAutenticationService)}::{nameof(GetAcessToken)}] - Exception: {ex}");
                throw;
            }
        } */
    }
}