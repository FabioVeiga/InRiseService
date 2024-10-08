using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Domain.Orders;
using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(int userId, decimal Price);
        Task<bool> UpdateAsync(int id, int orderStatusId);
        Task CreateItemsAsync(int orderId, IEnumerable<ProductDtoRequest> itemsRequest);
        Task CreateHistoricAsync(int orderId, int orderStatusId);
        Task<OrderStatus> GetOrderStatusById(int id);
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync();
        Task<OrderDtoResponse> GetOrdersById(int id);
        Task<IEnumerable<OrderDtoResponse>> GetOrdersByUserId(int id);
        Task<IEnumerable<OrderHistoricDto?>> GetOrderHistoricByNumber(int numberOrder);
        Task<IEnumerable<OrderHistoricDto?>> GetOrderHistoricByOrderId(int orderIdid);
    }
}