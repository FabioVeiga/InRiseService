using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.SoftwareDto
{
    public class SoftwareRequestDto 
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;
    }
}
