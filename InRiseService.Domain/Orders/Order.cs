using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.OrderStatuses;
using InRiseService.Domain.Users;

namespace InRiseService.Domain.Orders
{
    public class Order : BaseDomain
    {
        [Display(Name = "Numero Pedido")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Number { get; set; }

        [Display(Name = "Data Pedido")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public DateTime Date { get; set; }

        [Display(Name = "Data Estimada")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public DateTime DateEstimated { get; set; }

        [Display(Name = "Data Entrega")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public DateTime DateDelivered { get; set; }

        [Display(Name = "Desempenho")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Performace { get; set; }

        [JsonIgnore]
        public OrderStatus Status { get; set; } = default!;

        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; } = default!;
        
        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public decimal TotalValue { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } = default!;
        
        [JsonIgnore]
        public User User { get; set; } = default!;

        [JsonIgnore]
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}