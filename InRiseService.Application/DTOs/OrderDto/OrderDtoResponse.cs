using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Application.DTOs.OrderDto
{
    public class OrderDtoResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateEstimated { get; set; }
        public DateTime? DatePayment { get; set; }
        public DateTime? DateDelivered { get; set; }
        public int Performace { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public IList<OrderItemDtoResponse> OrderItems { get; set; } = new List<OrderItemDtoResponse>();
    }
    public class OrderItemDtoResponse
    {
        public string Nome { get; set; } = default!;
        public decimal Price { get; set; }
    }
}