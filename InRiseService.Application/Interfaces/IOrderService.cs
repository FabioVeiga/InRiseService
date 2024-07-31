using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Domain.Orders;
using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(int userId, decimal Price);
        Task CreateItemsAsync(int orderId, IEnumerable<ProductDtoRequest> itemsRequest);
        Task CreateHistoricAsync(int orderId, int orderStatusId);
        Task<OrderStatus> GetOrderStatusById(int id);
        
        Task<OrderDtoResponse> GetOrdersById(int id);
        Task<IEnumerable<OrderDtoResponse>> GetOrdersByUserId(int id);
    }
}