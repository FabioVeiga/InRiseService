using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using InRiseService.Domain.Categories;

namespace InRiseService.Domain.Processors
{
    public class Processor : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Geração")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Generation { get; set; } = default!;

        [Display(Name = "Socket")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Socket { get; set; } = default!;

        [Display(Name = "Nucleos")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Core { get; set; } = default!;

        [Display(Name = "Frequência")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Frequency { get; set; } = default!;

        [Display(Name = "Potência/Consumo")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Potency { get; set; } = default!;

        [JsonIgnore]
        public Category Category { get; set; } = default!;
    }
}