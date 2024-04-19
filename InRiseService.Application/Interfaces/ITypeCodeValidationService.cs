using InRiseService.Application.DTOs.ValidationCodeDto;

namespace InRiseService.Application.Interfaces
{
    public interface ITypeCodeValidationService
    {
        public IEnumerable<TypeValidationCodeDto> GetAll();
        public TypeValidationCodeDto? GetById(int id);
        public TypeValidationCodeDto? GetByName(string name);
    }
}