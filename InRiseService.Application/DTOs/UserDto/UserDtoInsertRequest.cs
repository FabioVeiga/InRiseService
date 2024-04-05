using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.UserDto
{
    public class UserDtoInsertRequest
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Apelido")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Nickname { get; set; } = default!;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "{0} deve ser um e-mail válido")]
        public string Email { get; set; } = default!;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool Marketing { get; set; }

        [Display(Name = "Termos")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool Term { get; set; }
    }
}