using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.TowerDto
{
    public class TowerDtoRequest
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Dimensão (mm)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Dimesion { get; set; } = default!;

        [Display(Name = "Numero Max Fans")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MaxFans { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Valor (Classificação)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ValueClassification { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public PriceRequestDto Price { get; set; } = default!;
    }
}