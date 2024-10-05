using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Application.Interfaces
{
    public interface IOrderStatusService
    {
        Task<List<OrderStatus>> Get();
        Task<OrderStatus?> GetByIdAsync(int id);
    }
}