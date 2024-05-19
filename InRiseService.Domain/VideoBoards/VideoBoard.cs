using System.ComponentModel.DataAnnotations;

namespace InRiseService.Domain.VideoBoards
{
    public class VideoBoard : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Bits")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Bits { get; set; } = default!;

        [Display(Name = "Capacidades (Gb)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Capacity { get; set; } = default!;

        [Display(Name = "Potência/Consumo")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Potency { get; set; } = default!;

    }
}