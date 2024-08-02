using System.ComponentModel.DataAnnotations;
using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Domain.Orders
{
    public class OrderHistoric
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public int OrderStatusId { get; set; }
        public OrderStatus Status { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}