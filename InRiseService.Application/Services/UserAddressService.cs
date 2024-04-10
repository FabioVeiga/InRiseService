using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.UsersAddress;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UserAddressService> _logger;

        public UserAddressService(ApplicationContext context,  ILogger<UserAddressService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserAddress> InsertAsync(UserAddress userAddress)
        {
            try
            {
                userAddress.InsertIn = DateTime.Now;
                _context.Add(userAddress);
                await _context.SaveChangesAsync();
                return userAddress;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserAddressService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}