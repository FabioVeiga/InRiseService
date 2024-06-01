using InRiseService.Domain.Enums;
using InRiseService.Domain.ValidationCodes;

namespace InRiseService.Application.Interfaces
{
    public interface IValidationCodeService
    {
        Task<ValidationCode> InsertAsync(int userId, EnumTypeCodeValidation enumTypeCodeValidation = EnumTypeCodeValidation.Email);
        Task<ValidationCode?> UpdateAsync(ValidationCode validationCode);
        Task<ValidationCode?> GetLastValideCodeByUserIdAsync(int userId);
        Task<ValidationCode?> GetLastValideCodeByCode(int code);
    }
}