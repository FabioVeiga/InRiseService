using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InRiseService.Application.DTOs.ProductDto;
using InRiseService.Domain.Products;
using InRiseService.Domain.Prices;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(
        ILogger<ProductController> logger,
        IMapper mapper,
        IProductService productService,
        IProductCategoryService productCategoryService,
        IImageService imageService
            ) : ControllerBase
    {
        private readonly ILogger<ProductController> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IProductService _productService = productService;
        private readonly IProductCategoryService _productCategoryService = productCategoryService;
        private readonly IImageService _imageService = imageService;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<Product>(request);
                var category = await _productCategoryService.GetByIdAsync(request.ProductCategoryId);
                if(category == null) return NotFound("Category not found");
                var result = await _productService.InsertAsync(mapped, request);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao inserir"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProductRequestDto request, int id)
        {
            try
            {
                var model = await _productService.GetByIdAsync(id);
                if(model is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                var category = await _productCategoryService.GetByIdAsync(request.ProductCategoryId);
                if(category == null) return NotFound("Category not found");
                var modelPrice = model.Price;
                model = _mapper.Map<Product>(request);
                model.Id = id;
                model.Price = _mapper.Map<Price>(request.Price);
                model.Price.Id = modelPrice!.Id;
                model.PriceId = modelPrice.Id;
                await _productService.UpdateAsync(model, request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao alterar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);
                if(result == null) return NotFound();

                var mappedResponse = _mapper.Map<ProductResponseDto>(result);
                mappedResponse.Category = new ProductCategoryResponseSimpleDto(){
                    Id = result.ProductCategory.Id,
                    Name = result.ProductCategory.Name,
                    Description = result.ProductCategory.Description
                };

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    mappedResponse
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao buscar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _productService.DeleteAsync(result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao deletar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFiltered([FromQuery]ProductFilterDto request)
        {
            try
            {
               var result = await _productService.GetByFilterAsync(request);
                if(result.TotalItems == 0)
                    return NotFound();

                foreach (var item in result.Items)
                {
                    var mappedResponse = _mapper.Map<ProductResponseDto>(item);
                    mappedResponse.Images = await _imageService.GetByProductIdAsync(item.Id);
                }

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao buscar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("Activate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                var model = await _productService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = true;
                var mapped = _mapper.Map<ProductRequestDto>(model);
                await _productService.UpdateAsync(model, mapped);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao ativar"
                    );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("Deactivate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                var model = await _productService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = false;
                var mapped = _mapper.Map<ProductRequestDto>(model);
                await _productService.UpdateAsync(model, mapped);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao desativar"
                    );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}