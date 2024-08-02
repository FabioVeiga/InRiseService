using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.Enums;

namespace InRiseService.Domain.Orders
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        public EnumTypeCategoryImage ProductType { get; set; }
        public int ProductId { get; set; } = default!;
        public decimal Price { get; set; }
        public int OrderId { get; set; } = default!;
    }
}