using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Categories;

namespace InRiseService.Domain.Softwares
{
    public class Software : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ProcessadorMinId { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ProcessadorIdealId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRamMinId { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRamIdealId { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int VideoBoardMinId { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int VideoBoardIdealId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; } = default!;

    }
}