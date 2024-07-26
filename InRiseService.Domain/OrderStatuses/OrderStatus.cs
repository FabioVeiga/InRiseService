using System.ComponentModel.DataAnnotations;

namespace InRiseService.Domain.OrderStatuses
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsSendEmail { get; set; } = false;
        public bool IsVisibleToUser { get; set; } = false;
    }
}