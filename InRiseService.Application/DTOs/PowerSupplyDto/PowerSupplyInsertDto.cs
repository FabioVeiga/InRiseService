using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.PowerSupplyDto
{
    public class PowerSupplyInsertDto
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Potência/Consumo W")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Potency { get; set; }

        [Display(Name = "Potência Real")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int PotencyReal { get; set; }

        [Display(Name = "Selo")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public string Stamp { get; set; } = default!;

        [Display(Name = "Modular")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool Modular { get; set; }
    }
}