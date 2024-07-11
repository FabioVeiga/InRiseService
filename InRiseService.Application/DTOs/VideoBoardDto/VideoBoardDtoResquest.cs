using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.VideoBoardDto
{
    public class VideoBoardDtoResquest
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
        public int Potency { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public PriceRequestDto Price { get; set; } = default!;
    }
}