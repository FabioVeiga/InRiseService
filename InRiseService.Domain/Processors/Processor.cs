using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Prices;

namespace InRiseService.Domain.Processors
{
    public class Processor : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Geração")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Generation { get; set; } = default!;

        [Display(Name = "Socket Processador")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Socket { get; set; } = default!;

        [Display(Name = "Números Nucleos")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(10, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public int Core { get; set; }

        [Display(Name = "Frequência GHz")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public double Frequency { get; set; }

        [Display(Name = "Potência/Consumo W")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(10, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public int Potency { get; set; }

        [Display(Name = "Suporte RAM")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SuportMemoryRAM { get; set; } = default!;

        [Display(Name = "Suporte SSD/HD")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SuportMemoryROM { get; set; } = default!;

        [Display(Name = "Suporte Video")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SuportVideo { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        public int PriceId { get; set; }
        public Price? Price { get; set; }
    }
}