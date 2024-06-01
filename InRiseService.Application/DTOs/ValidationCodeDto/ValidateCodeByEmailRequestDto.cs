using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.ValidationCodeDto
{
    public class ValidateCodeByEmailRequestDto
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Code { get; set; }
        
    }
}