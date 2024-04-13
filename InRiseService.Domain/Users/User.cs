using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using InRiseService.Domain.Enums;
using InRiseService.Domain.UsersAddress;

namespace InRiseService.Domain.Users
{
    public class User : BaseDomain
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
        public string Lastname { get; set; } = default!;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "{0} deve ser um e-mail válido")]
        public string Email { get; set; } = default!;

        [Display(Name = "E-mail Valido")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool EmailValide { get; set; } = false;

        [Display(Name = "Telemóvel")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string PhoneNumber { get; set; } = default!;

        [Display(Name = "Telemóvel Valido")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool PhoneNumberValide { get; set; } = false;

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

        [JsonIgnore]
        public ICollection<UserAddress> Address { get; set; }

        public User(){}

        public User(string name, string lastname, string email, string phoneNumber, string password, EnumProfile profile, bool marketing, bool term) 
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Profile = profile;
            Marketing = marketing;
            Term = term;
            Active = true;
        }
    }
}