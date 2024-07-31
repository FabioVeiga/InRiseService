using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Enums;
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

        public async Task<OrderDtoResponse> GetOrdersById(int id)
        {
            try
            {
                var result = new OrderDtoResponse();
                var model = await _context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
                if (model == null) return result;

                result.Id = model.Id;
                result.Number = model.Number;
                result.Date = model.Date;
                result.DateEstimated = model.DateEstimated;
                result.DateDelivered = model.DateDelivered;
                result.StatusId = model.Status.Id;
                result.Status = model.Status.Name;
                result.TotalPrice = model.TotalValue;

                foreach (var item in model.OrderItems)
                {
                    var orderItem = new OrderItemDtoResponse
                    {
                        Price = item.Price,
                        Nome = GetProductName(item.ProductId, item.ProductType)
                    };
                    result.OrderItems.Add(orderItem);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(OrderService), nameof(GetOrdersById), ex);
                throw;
            }
        }

        public async Task<IEnumerable<OrderDtoResponse>> GetOrdersByUserId(int id)
        {
            
            try
            {
                var result = new List<OrderDtoResponse>();
                var model = await _context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Include(x => x.Status)
                .Where(x => x.UserId == id)
                .ToListAsync();
                
                var orders = model.Select(order => new OrderDtoResponse
                {
                    Id = order.Id,
                    Number = order.Number,
                    Date = order.Date,
                    DateEstimated = order.DateEstimated,
                    DateDelivered = order.DateDelivered,
                    StatusId = order.Status.Id,
                    Status = order.Status.Name,
                    TotalPrice = order.TotalValue,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDtoResponse
                    {
                        Price = item.Price,
                        Nome = GetProductName(item.ProductId, item.ProductType)
                    }).ToList()
                });

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(OrderService), nameof(GetOrdersById), ex);
                throw;
            }
        }

        private string GetProductName(int productId, EnumTypeCategoryImage productType)
        {
            switch (productType)
            {
                case EnumTypeCategoryImage.cooler:
                    return _context.Coolers.FirstOrDefault(x => x.Id == productId)?.Name ?? string.Empty;
                case EnumTypeCategoryImage.memoryRam:
                    return _context.MemoriesRam.FirstOrDefault(x => x.Id == productId)?.Name ?? string.Empty;
                default:
                    return string.Empty;
            }
        }
    
    }
}