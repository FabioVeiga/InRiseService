using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Application.DTOs.ProductCategoryDto;
using InRiseService.Application.DTOs.ProductDto;
using InRiseService.Application.Extentions;
using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.ProductCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class ProductCategoryService(ApplicationContext context, ILogger<ProductCategoryService> logger) : IProductCategoryService
    {
        private readonly ApplicationContext _context = context;
        private readonly ILogger<ProductCategoryService> _logger = logger;

        public async Task DeleteAsync(ProductCategory category)
        {
            try
            {
                category.DeleteIn = DateTime.Now;
                category.Active = false;
                _context.Update(category);
                await UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductCategoryService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<Pagination<ProductCategoryResponseDto>> GetByFilterAsync(ProductCategoryFilterDto filter)
        {
            try
            {
                var query = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Price)
                .AsNoTracking()
                .Where(p => p.Name.ToUpper().Contains(filter.Name)
                );

                if (filter.IsActive.HasValue)
                    query = query.Where(x => x.Active == filter.IsActive.Value);

                if (filter.IsDeleted.HasValue)
                    query = filter.IsDeleted.Value
                        ? query.Where(x => x.DeleteIn != null)
                        : query.Where(x => x.DeleteIn == null);

                var listResultDto = query.Select(x => new ProductCategoryResponseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active,
                    InsertIn = x.InsertIn,
                    DeleteIn = x.DeleteIn,
                    UpdateIn = x.UpdateIn,
                    Description = x.Description,
                    Products = x.Products.Select(p => new ProductResponseDto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        InsertIn = x.InsertIn,
                        DeleteIn = x.DeleteIn,
                        UpdateIn = x.UpdateIn,
                        Description = x.Description,
                        Price = new PriceResponseDto()
                        {
                            Id = p.Price!.Id,
                            CostPrice = p.Price.CostPrice,
                            FinalPrice = p.Price.FinalPrice,
                            IVA = p.Price.IVA,
                            PorcentageADMCost = p.Price.PorcentageADMCost,
                            PorcentageDiscount = p.Price.PorcentageDiscount,
                            PorcentageFixedCost = p.Price.PorcentageFixedCost,
                            PorcentageProfit = p.Price.PorcentageProfit,
                        },
                        ValueTypeProducts = p.ValueTypeProducts
                    }).ToList()
                });

                var finalListResult = await listResultDto.PaginationAsync(filter.Pagination.PageIndex, filter.Pagination.PageSize);
                return finalListResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductCategoryService), nameof(GetByFilterAsync), ex);
                throw;
            }
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ProductCategories
                .Include(x => x.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProductCategoryService), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task<ProductCategory> InsertAsync(ProductCategory category)
        {
            try
            {
                category.InsertIn = DateTime.Now;
                category.Active = true;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProcessorService), nameof(InsertAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(ProductCategory category)
        {
            try
            {
                category.UpdateIn = DateTime.Now;
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{Service}::{Method}] - Exception: {Ex}", nameof(ProcessorService), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}