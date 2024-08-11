using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MemoryRamDto
{
    public class MemoryRamRequestDto
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

        [Display(Name = "Frequência MHz")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public double Frequency { get; set; }

        [Display(Name = "Capacidade Gb")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Capacity { get; set; }

        [Display(Name = "Dimensões (mm)")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Valor (Classificação)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ValueClassification { get; set; }

        public PriceRequestDto Price { get; set; } = default!;
    }
}