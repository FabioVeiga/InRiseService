using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Prices;

namespace InRiseService.Domain.VideoBoards
{
    public class VideoBoard : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Socket")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Socket { get; set; } = default!;

        [Display(Name = "Bits")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Bits { get; set; } = default!;

        [Display(Name = "Capacidades (Gb)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Capacity { get; set; } = default!;

        [Display(Name = "Dimensão (mm)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Dimension { get; set; } = default!;

        [Display(Name = "Potência/Consumo")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public int Potency { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Valor (Classificação)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ValueClassification { get; set; }

        public int PriceId { get; set; }
        public Price? Price { get; set; }

    }
}