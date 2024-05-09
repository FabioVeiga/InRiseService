using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.CategoryDto
{
    public class CategoryDtoRequest
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public string Name { get; set; } = default!;
    }
}