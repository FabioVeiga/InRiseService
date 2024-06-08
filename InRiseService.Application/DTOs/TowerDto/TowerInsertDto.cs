using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.TowerDto
{
    public class TowerInsertDto
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
        [MinLength(1,ErrorMessage = "{0} de ter no mínimo {1}")]
        public int MaxFans { get; set; } = default!;
    }
}