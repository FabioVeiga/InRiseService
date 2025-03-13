using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.ProductDto
{
    public class ProductRequestDto
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public List<EnumValueTypeProduct> ValueTypeProducts { get; set; } = [];

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public PriceRequestDto Price { get; set; } = default!;
    }
}