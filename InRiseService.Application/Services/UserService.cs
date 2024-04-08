using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Users;
using InRiseService.Util;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> CheckEmailIfExists(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(CheckEmailIfExists)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User?> CheckPhoneNumberIfExists(string phoneNumber)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(CheckEmailIfExists)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User> InsertAsync(User user)
        {
            try
            {
                user.InsertIn = DateTime.Now;
                user.Password = PasswordHelper.EncryptPassword(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                user.UpdateIn = DateTime.Now;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                    
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(GetByIdAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User> DeleteAsync(User user)
        {
            try
            {
                user.DeleteIn= DateTime.Now;
                user.Active = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(DeleteAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User> ActivateAsync(User user)
        {
            try
            {
                user.DeleteIn = null;
                user.Active = true;
                user.UpdateIn= DateTime.Now;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(ActivateAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<User> DectivateAsync(User user)
        {
            try
            {
                user.Active = false;
                user.UpdateIn = DateTime.Now;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(DectivateAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}