using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Users;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> InsertAsync(User user)
        {
            try
            {
                user.InsertIn = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                throw;
            }
        }
    }
}