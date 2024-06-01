using System.ComponentModel.DataAnnotations;

namespace InRiseService.Domain.MemoriesRam
{
    public class MemoryRam : BaseDomain
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
    }
}