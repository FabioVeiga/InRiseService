using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.UsersAddress;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

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

        public async Task<UserAddress?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.UserAddresses.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserAddressService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<UserAddress>>  GetByUserIdAsync(int userId)
        {
            try
            {
                var list = await _context.UserAddresses
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
                if(list.Count== 0)
                    return Enumerable.Empty<UserAddress>();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserAddressService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
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
                userAddress.Active = true;
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
                address.UpdateIn = DateTime.Now;
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