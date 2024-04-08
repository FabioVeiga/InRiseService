using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.UserDto
{
    public class UserDtoUpdateRequest
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Apelido")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Lastname { get; set; } = default!;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "{0} deve ser um e-mail válido")]
        public string Email { get; set; } = default!;

        [Display(Name = "Telemóvel")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "{0}  não está no formato correto! ex: +44 20 1234 5678")]
        public string PhoneNumber { get; set; } = default!;

        [Display(Name = "Senha")]
        public string? Password { get; set; }

    }
}