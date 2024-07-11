using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Prices;

namespace InRiseService.Domain.PowerSupplies
{
    public class PowerSupply : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Potência/Consumo W")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public int Potency { get; set; }

        [Display(Name = "Potência Real")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public int PotencyReal { get; set; }

        [Display(Name = "Selo")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public string Stamp { get; set; } = default!;

        [Display(Name = "Modular")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool Modular { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        public int PriceId { get; set; }
        public Price? Price { get; set; }


    }
}