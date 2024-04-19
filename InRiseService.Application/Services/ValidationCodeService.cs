using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Enums;
using InRiseService.Domain.ValidationCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class ValidationCodeService : IValidationCodeService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ValidationCodeService> _logger;

        public ValidationCodeService(ApplicationContext context, ILogger<ValidationCodeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ValidationCode?> GetLastValideCodeByCode(int code)
        {
            try
            {
                var model = await _context.ValidationCodes
                .Include(v => v.User)
                .FirstOrDefaultAsync(x => x.Code == code);
                if(model is null) return null;
                if(DateTime.Now > model.ExpirateAt) return null;
                if(model.IsValidate) return null;
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ValidationCodeService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<ValidationCode?> GetLastValideCodeByUserIdAsync(int userId)
        {
            try
            {
                var model = await _context.ValidationCodes.FirstOrDefaultAsync(x => x.UserId == userId);
                if(model is null) return null;
                if(DateTime.Now > model.ExpirateAt) return null;
                if(model.IsValidate) return null;
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ValidationCodeService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<ValidationCode> InsertAsync(int userId, EnumTypeCodeValidation enumTypeCodeValidation)
        {
            try
            {
                var model = new ValidationCode()
                {
                    Code = Util.IntegerHelper.GenerateRandomSixDigitNumber(),
                    UserId = userId,
                    TypeCode = enumTypeCodeValidation,
                    Active = true
                };
                _context.ValidationCodes.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ValidationCodeService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<ValidationCode?> UpdateAsync(ValidationCode validationCode)
        {
            try
            {
                validationCode.UpdateIn = DateTime.Today;
                _context.ValidationCodes.Update(validationCode);
                await _context.SaveChangesAsync();
                return validationCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ValidationCodeService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}