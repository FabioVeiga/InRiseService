using InRiseService.Application.DTOs.ValidationCodeDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class TypeCodeValidationService : ITypeCodeValidationService
    {

        private readonly ILogger<TypeCodeValidationService> _logger;

        public TypeCodeValidationService(ILogger<TypeCodeValidationService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<TypeValidationCodeDto> GetAll()
        {
            try
            {
                var resultListProfileDto = new List<TypeValidationCodeDto>();
                var enumValues = Enum.GetValues(typeof(EnumTypeCodeValidation));
                foreach (EnumTypeCodeValidation value in enumValues)
                {
                    resultListProfileDto.Add(new TypeValidationCodeDto(){
                        Id = (int)value,
                        Name = value.ToString()
                    });
                }
                return resultListProfileDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(TypeCodeValidationService)}::{nameof(GetAll)}] - Exception: {ex}");
                throw;
            }
        }

        public TypeValidationCodeDto? GetById(int id)
        {
            try
            {
                return GetAll().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(TypeCodeValidationService)}::{nameof(GetById)}] - Exception: {ex}");
                throw;
            }
        }

        public TypeValidationCodeDto? GetByName(string name)
        {
            try
            {
                return GetAll().FirstOrDefault(x => x.Name.ToUpper().Contains(name.ToUpper()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(TypeCodeValidationService)}::{nameof(GetByName)}] - Exception: {ex}");
                throw;
            }
        }
    }
}