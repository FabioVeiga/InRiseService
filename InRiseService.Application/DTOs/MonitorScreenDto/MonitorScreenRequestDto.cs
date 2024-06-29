using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MonitorScreenDto
{
    public class MonitorScreenRequestDto
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Dimensão (pol)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Dimesion { get; set; } = default!;

        [Display(Name = "Taxa de Atualização")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int UpdateVolume { get; set; } = default!;

        [Display(Name = "Qualidade")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string Quality { get; set; } = default!;

        public PriceRequestDto Price { get; set; } = default!;
    }
}