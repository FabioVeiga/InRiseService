using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InRiseService.Application.DTOs.CategoryDto;
using InRiseService.Domain.Categories;
using InRiseService.Application.DTOs.SoftwareDto;
using InRiseService.Domain.Softwares;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly ILogger<SoftwareController> _logger;
        private readonly IMapper _mapper;
        private readonly ISoftwareService _softwareService;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;

        public SoftwareController(
            ILogger<SoftwareController> logger,
            IMapper mapper,
            ISoftwareService processorService,
            IImageService imageService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _mapper = mapper;
            _softwareService = processorService;
            _imageService = imageService;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SoftwareRequestDto request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<Software>(request);
                var category = await _categoryService.GetByIdAsync(request.CategoryId);
                if(category == null)
                {
                    ModelState.AddModelError(nameof(request.CategoryId), "Não encontrado!");
                    return NotFound(new ValidationProblemDetails(ModelState));
                }
                var result = await _softwareService.InsertAsync(mapped);
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

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _softwareService.GetByIdAsync(id);
                if(result == null) return NotFound();

                var mappedResponse = _mapper.Map<SoftwareResponseDto>(result);
                mappedResponse.Images = await _imageService.GetBySoftwareIdAsync(result.Id);

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
                var result = await _softwareService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _softwareService.DeleteAsync(result);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFiltered([FromQuery]SoftwareFilterDto request)
        {
            try
            {
               var result = await _softwareService.GetByFilterAsync(request);
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
    }
}