using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MemoryRomDto
{
    public class MemoryRomRequestDto
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

        [Display(Name = "Velocidade da Leitura (Mb/s)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public double VelocityRead { get; set; } = default!;

        [Display(Name = "Velocidade da Gravação (Mb/s)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public double VelocityWrite { get; set; } = default!;

        [Display(Name = "Capacidade Gb")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Capacity { get; set; }

        [Display(Name = "Potência/Consumo W")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Potency { get; set; }

        [Display(Name = "HHD")]
        public bool IsHHD { get; set; } = false;

        [Display(Name = "SSD")]
        public bool IsSSD { get; set; } = true;

        [Display(Name = "SSDM2")]
        public bool IsSSDM2 { get; set; } = false;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Valor (Classificação)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ValueClassification { get; set; }

        public PriceRequestDto Price { get; set; } = default!;
    }
}