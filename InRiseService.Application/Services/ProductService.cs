using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.DTOs.ProductDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class ProductService(ApplicationContext context, ILogger<ProductService> logger) : IProductService
    {
        private readonly ApplicationContext _context = context;
        private readonly ILogger<ProductService> _logger = logger;

        public async Task DeleteAsync(Product product)
        {
            try
            {
                product.DeleteIn = DateTime.Now;
                product.Active = false;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<ProductResponseDto>> GetByFilterAsync(ProductFilterDto filter)
        {
            try
            {
                var query = _context.Processors
                .Include(x => x.Price)
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name)
                );

                if (filter.IsActive.HasValue)
                    query = query.Where(x => x.Active == filter.IsActive.Value);

                if (filter.IsDeleted.HasValue)
                    query = filter.IsDeleted.Value 
                        ? query.Where(x => x.DeleteIn != null) 
                        : query.Where(x => x.DeleteIn == null);

                var listResultDto = query.Select(x => new ProductResponseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    InsertIn = x.InsertIn,
                    DeleteIn = x.DeleteIn,
                    UpdateIn = x.UpdateIn,
                    Description = x.Description,
                    Price = new PriceResponseDto(){
                        Id = x.Price!.Id,
                        CostPrice = x.Price.CostPrice,
                        FinalPrice = x.Price.FinalPrice,
                        IVA = x.Price.IVA,
                        PorcentageADMCost = x.Price.PorcentageADMCost,
                        PorcentageDiscount = x.Price.PorcentageDiscount,
                        PorcentageFixedCost = x.Price.PorcentageFixedCost,
                        PorcentageProfit = x.Price.PorcentageProfit,
                    }
                });

                var finalListResult = await listResultDto.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Products
                .Include(x => x.Price)
                .Include(x => x.ProductCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<Product> InsertAsync(Product product, ProductRequestDto dto)
        {
            try
            {
                product.InsertIn = DateTime.Now;
                product.Active = true;
                product.SetValueTypeProducts(dto.ValueTypeProducts);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(Product product, ProductRequestDto dto)
        {
            try
            {
                product.UpdateIn = DateTime.Now;
                _context.Add(product);
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}