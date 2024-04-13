using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.UserAddressDto
{
    public class UserAddressDtoInsertRequest
    {
        [Display(Name = "Pais")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(2, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(2, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string CountryCode { get; set; } = default!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string State { get; set; } = default!;

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Province { get; set; } = default!;

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string City { get; set; } = default!;

        [Display(Name = "PostalCode")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string PostalCode { get; set; } = default!;

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public string Street { get; set; } = default!;

        [Display(Name = "Numero")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Number { get; set; } = default!;

        [Display(Name = "Observação")]
        public string Observation { get; set; } = default!;

        [Display(Name = "Padrão")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool IsDefault { get; set; } = default!;

        [Display(Name = "Faturação")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool IsBilling { get; set; } = default!;

        public int UserId { get; set; } = default!;
    }
}
