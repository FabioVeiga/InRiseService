using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.UsersAddress;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserAddress?> GetCurrentDefaultAsync(int userId)
        {
            try
            {
                return await _context.UserAddresses.FirstOrDefaultAsync(x => x.UserId == userId && x.IsDefault);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserAddressService)}::{nameof(GetCurrentDefaultAsync)}] - Exception: {ex}");
                throw;
            }
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

        public async Task<UserAddress> UpdateAsync(UserAddress address)
        {
            try
            {
                _context.UserAddresses.Update(address);
                await _context.SaveChangesAsync();
                return  address;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserAddressService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}