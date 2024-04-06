using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.UserDto
{
    public class UserDtoInsertRequest : IValidatableObject
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

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public EnumProfile Profile { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool Marketing { get; set; }

        [Display(Name = "Termos")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool Term { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!Term)
                results.Add(new ValidationResult("É necessário aceitar os termos para continuar.", new[] { nameof(Term) }));
            return results;
        }
    }
}