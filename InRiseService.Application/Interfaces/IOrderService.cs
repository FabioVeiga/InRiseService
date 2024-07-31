using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Domain.Orders;
using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(int userId);
        Task CreateItemsAsync(int orderId, IEnumerable<ProductDtoRequest> itemsRequest);
        Task CreateHistoricAsync(int orderId, int orderStatusId);
        Task<OrderStatus> GetOrderStatusById(int id);
    }
}