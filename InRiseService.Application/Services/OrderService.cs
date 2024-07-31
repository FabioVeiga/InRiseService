using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Orders;
using InRiseService.Domain.OrderStatuses;
using InRiseService.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ApplicationContext context,  ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<Order> CreateAsync(int userId, decimal Price)
        {
            try
            {
                var model = new Order(){
                    UserId = userId,
                    OrderStatusId = 1,
                    Date =  DateTime.Now,
                    Number = await GetNumberOrder(),
                    InsertIn = DateTime.Now,
                    TotalValue = Price
                };
                _context.Orders.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(OrderService), nameof(CreateAsync), ex);
                throw;
            }
        }

        public async Task<OrderStatus> GetOrderStatusById(int id)
        {
            var model = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Id == id);
            if(model is not null) return model;
            return new OrderStatus(){
                Id = 1,
                Name = "Aguardando Pagamento",
                IsSendEmail = true,
                IsVisibleToUser = true
            };
        }

        private async Task<int> GetNumberOrder()
        {
            try
            {
                _context.ChangeTracker.Clear();
                var number = IntegerHelper.GenerateRandomSixDigitNumber();
                var hasNumber = await _context.Orders.FirstOrDefaultAsync(x => x.Number == number);
                if(hasNumber == null)
                    return number;
                else
                    return await GetNumberOrder();

            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(OrderService), nameof(GetNumberOrder), ex);
                throw;
            }
        }

        public async Task CreateItemsAsync(int orderId, IEnumerable<ProductDtoRequest> itemsRequest)
        {
            try
            {
                var list = new List<OrderItems>();
                foreach (var item in itemsRequest)
                {
                    var itemModel = new OrderItems()
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        ProductType = item.TypeCategory,
                        OrderId = orderId
                    };
                    list.Add(itemModel);
                }
                _context.OrderItems.AddRange(list);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(OrderService), nameof(CreateItemsAsync), ex);
                throw;
            }
        }

        public Task CreateHistoricAsync(int orderId, int orderStatusId)
        {
            try
            {
                var model = new OrderHistoric(){
                    OrderId = orderId,
                    OrderStatusId = orderStatusId
                };
                _context.OrderHistorics.Add(model);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(OrderService), nameof(CreateItemsAsync), ex);
                throw;
            }
        }

    }
}