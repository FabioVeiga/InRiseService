using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.PaymentDto
{
    public class PaymentProductsRequestDto
    {
        [Required]
        public List<PaymentProductDto> ProductDtos { get; set; } = [];
    }

    public class PaymentProductDto{
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}