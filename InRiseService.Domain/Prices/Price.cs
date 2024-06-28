using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.MemoriesRam;

namespace InRiseService.Domain.Prices
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Preço de Custo")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "% de Lucro (L)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal PorcentageProfit { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "% referente Custo Fixo (CF)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal PorcentageFixedCost { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "% referente Custo ADM (CA)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal PorcentageADMCost { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "% Desconto (D)")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal PorcentageDiscount { get; set; } = 0.0m;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Subtotal")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "IVA")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal IVA { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Preço de Venda")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal FinalPrice { get; set; }

        public ICollection<Cooler>? Coolers { get; set; }
        public ICollection<MemoryRam>? MemoriesRam { get; set; }
    }
}