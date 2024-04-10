using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InRiseService.Application.DTOs.ApiSettingDto;
using InRiseService.Application.DTOs.UserAutenticationDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InRiseService.Application.Services
{
    public class UserAutenticationService : IUserAutenticationService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UserAutenticationService> _logger;
        private readonly AppSetting _appSetting;

        public UserAutenticationService(ApplicationContext context, ILogger<UserAutenticationService> logger, IOptions<AppSetting> options)
        {
            _context = context;
            _logger = logger;
            _appSetting = options.Value;
        }

        public UserAutenticationAcessTokenDtoResponse GetTokenAsync(UserAutenticationDtoRequest request)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, request.Email),
                        new Claim(ClaimTypes.Role, request.Profile.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(_appSetting.AcessTokenTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new UserAutenticationAcessTokenDtoResponse(tokenHandler.WriteToken(token), _appSetting.AcessTokenTime);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserAutenticationService)}::{nameof(GetTokenAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}