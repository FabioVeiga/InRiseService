using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.Interfaces;
using InRiseService.Application.UserDto;
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

        public async Task<IEnumerable<UserDtoResponse>> GetUserByFilter(UserDtoFilterRequest request)
        {
            try
            {
                var result = await _context.Users
                    .Where(x => x.Name.ToUpper().Contains(request.Name)
                    && x.Lastname.ToUpper().Contains(request.Lastname)
                    && x.PhoneNumber.Contains(request.PhoneNumber)
                    && x.Email.ToUpper().Contains(request.Email)
                ).ToListAsync();
                if(request.Deleted.HasValue)
                    result = result.Where(x => x.DeleteIn is not null).ToList();
                if(request.Active.HasValue)
                    result = result.Where(x => x.Active == request.Active).ToList();
                if (request.Marketing.HasValue)
                    result = result.Where(x => x.Marketing == request.Marketing).ToList();

                var listResultDto = result.Select(x => new UserDtoResponse()
                {
                    DeleteIn = x.DeleteIn,
                    Active = x.Active,
                    Email = x.Email,
                    Id = x.Id,
                    InsertIn = x.InsertIn,
                    Marketing = x.Marketing,
                    Lastname = x.Lastname,
                    Name = x.Name,
                    UpdateIn = x.UpdateIn,
                    PhoneNumber = x.PhoneNumber
                });
                return listResultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(UserService)}::{nameof(UserDtoResponse)}] - Exception: {ex}");
                throw;
            }
        }
    }
}