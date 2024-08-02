using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Enums;

namespace InRiseService.Application.DTOs.OrderDto
{
    public class OrderDtoRequest
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Userid { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public IEnumerable<ProductDtoRequest> ProductDtoRequests { get; set; } = new List<ProductDtoRequest>();

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public decimal TotalPrice { get; set; }
    }

    public class ProductDtoRequest
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public EnumTypeCategoryImage TypeCategory { get; set; } = default!;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public decimal Price { get; set; }

    }
}