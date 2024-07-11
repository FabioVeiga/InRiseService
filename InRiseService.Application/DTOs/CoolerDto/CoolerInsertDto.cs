using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MemoryRamDto
{
    public class CoolerInsertDto
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Air")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Air { get; set; } = default!;

        [Display(Name = "Refrigeração")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Refrigeration { get; set; } = default!;

        [Display(Name = "Quantidade Fans")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int FanQuantity { get; set; } = default!;

        [Display(Name = "Diametro das Fans (mm)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int FanDiametric { get; set; } = default!;

        [Display(Name = "Velocidade Máx (rpm)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MaxVelocit { get; set; } = default!;

        [Display(Name = "Dimensões (mm)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Dimension { get; set; } = default!;
        
        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        public PriceRequestDto Price { get; set; } = default!;
    }
}