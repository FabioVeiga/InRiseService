using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Users;

namespace InRiseService.Domain.UsersAddress
{
    public class UserAddress : BaseDomain
    {
        [Display(Name = "Pais")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Country { get; set; } = default!;

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string City { get; set; } = default!;

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public string Street { get; set; } = default!;

        [Display(Name = "Numero")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(10, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public int Number { get; set; } = default!;

        [Display(Name = "Região")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Region { get; set; } = default!;

        [Display(Name = "PostalCode")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string PostalCode { get; set; } = default!;

        [Display(Name = "Observação")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Observation { get; set; } = default!;

        public int UserId { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}