using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.OrderStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<OrderStatusService> _logger;

        public OrderStatusService(ApplicationContext context,  ILogger<OrderStatusService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<List<OrderStatus>> Get()
        {
            try
            {
                return await _context.OrderStatuses.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{OrderStatusService}::{Get}] - Exception: {Ex}", nameof(OrderStatusService), nameof(Get), ex);
                throw;
            }
        }

        public async Task<OrderStatus?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{OrderStatusService}::{Get}] - Exception: {Ex}", nameof(OrderStatusService), nameof(GetByIdAsync), ex);
                throw;
            }
        }
    }
}