namespace InRiseService.Application.DTOs.OrderDto
{
    public class OrderHistoricDto
    {
        public int Number { get; set; }
        public string Status { get; set; } = default!;
        public DateTime Data { get; set; }
    }
}