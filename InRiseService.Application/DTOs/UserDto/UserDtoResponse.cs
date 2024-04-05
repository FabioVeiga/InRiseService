using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.UserDto
{
    public class UserDtoResponse
    {
        [Display(Name = "Nome")]
        public string Name { get; set; } = default!;

        [Display(Name = "Apelido")]
        public string Nickname { get; set; } = default!;

        [Display(Name = "E-mail")]
        public string Email { get; set; } = default!;

        [Display(Name = "Senha")]
        public string Password { get; set; } = default!;
        public bool Marketing { get; set; }

        [Display(Name = "Termos")]
        public bool Term { get; set; }
    }
}