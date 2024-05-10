using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> _logger;
        private readonly IMapper _mapper;
        private readonly IProcessorService _processorService;
        private readonly ICategoryService _categoryService;

        public ProcessorController(
            ILogger<ProcessorController> logger,
            IMapper mapper,
            IProcessorService processorService,
            ICategoryService categoryService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _processorService = processorService;
            _categoryService = categoryService;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProcessorDtoInsertRequest request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var category = await _categoryService.GetByIdAsync(request.CategoryId);
                if(category is null)
                {
                    ModelState.AddModelError(nameof(request.CategoryId), "Informar um Id que existe!!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var mapped = _mapper.Map<Processor>(request);
                mapped.CategoryId = category.Id;
                var result = await _processorService.InsertAsync(mapped);
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
                   "Erro ao inserir"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProcessorDtoInsertRequest request, int id)
        {
            try
            {
                var processor = await _processorService.GetByIdAsync(id);
                if(processor is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                var category = await _categoryService.GetByIdAsync(request.CategoryId);
                if(category is null)
                {
                    ModelState.AddModelError(nameof(request.CategoryId), "Informar um Id que existe!!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                processor.Category = category;
                processor.Core = request.Core;
                processor.Frequency = request.Frequency;
                processor.Generation = request.Generation;
                processor.Name = request.Name;
                processor.Socket = request.Socket;
                processor.Potency = request.Potency;
                await _processorService.UpdateAsync(processor);
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
        //[Authorize(Roles = "Admin)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _processorService.GetByIdAsync(id);
                if(result == null) return NotFound();

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

        [HttpGet]
        //[Authorize(Roles = "Admin)]
        public async Task<IActionResult> GetFiltered([FromBody] ProcessorDtoFilterRequest request)
        {
            try
            {
               var result = await _processorService.GetByFilterAsync(request);
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