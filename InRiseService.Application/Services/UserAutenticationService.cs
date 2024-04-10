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
    }
}