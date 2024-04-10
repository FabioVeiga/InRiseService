
using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.UserAutenticationDto
{
    public class UserAutenticationDtoRequest
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "{0} deve ser um e-mail válido")]
        public string Email { get; set; } = default!;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string Password { get; set; } = default!;

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public EnumProfile Profile { get; set; } = EnumProfile.User;

        internal string Secret { get; set; } = string.Empty;
    }
}