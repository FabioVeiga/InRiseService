using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InRiseService.Application.DTOs.ProductCategoryDto;
using InRiseService.Domain.ProductCategories;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(
            ILogger<ProductCategoryController> logger,
            IMapper mapper,
            IProductCategoryService productCategoryService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductCategoryRequestDto request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<ProductCategory>(request);
                var result = await _productCategoryService.InsertAsync(mapped);
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
        public async Task<IActionResult> Update([FromBody] ProductCategoryRequestDto request, int id)
        {
            try
            {
                var model = await _productCategoryService.GetByIdAsync(id);
                if(model is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                model = _mapper.Map<ProductCategory>(request);
                model.Id = id;
                await _productCategoryService.UpdateAsync(model);
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
                var result = await _productCategoryService.GetByIdAsync(id);
                if(result == null) return NotFound();

                var mappedResponse = _mapper.Map<ProductCategoryResponseDto>(result);

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
                var result = await _productCategoryService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _productCategoryService.DeleteAsync(result);
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
        public async Task<IActionResult> GetFiltered([FromQuery]ProductCategoryFilterDto request)
        {
            try
            {
               var result = await _productCategoryService.GetByFilterAsync(request);
                if(result.TotalItems == 0)
                    return NotFound();

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
                var model = await _productCategoryService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = true;
                await _productCategoryService.UpdateAsync(model);
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
                var model = await _productCategoryService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = false;
                await _productCategoryService.UpdateAsync(model);
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