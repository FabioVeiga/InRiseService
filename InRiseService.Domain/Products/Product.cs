using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Enums;
using InRiseService.Domain.Prices;
using InRiseService.Domain.ProductCategories;

namespace InRiseService.Domain.Products
{
    public class Product : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;
        
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string ValueTypeProducts { get; private set; } = string.Empty;

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = default!;

        public int PriceId { get; set; }
        public Price? Price { get; set; }

        public void SetValueTypeProducts(List<EnumValueTypeProduct> valueTypeProducts){
            ValueTypeProducts = string.Join(";", valueTypeProducts);
        }
    }
}