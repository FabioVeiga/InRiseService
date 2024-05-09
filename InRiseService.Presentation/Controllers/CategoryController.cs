using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.CategoryDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Categories;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, 
        IMapper mapper,
        ICategoryService categoryService)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDtoRequest request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                if(await _categoryService.GetByNameAsync(request.Name) is not null)
                {
                    ModelState.AddModelError(nameof(request.Name), "Já existe uam cattegoria com este nome!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var result = await _categoryService.InsertAsync(request.Name);
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
        [Route("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDtoRequest request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var hasName = await _categoryService.GetByNameAsync(request.Name);
                if(hasName is not null && hasName.Id != id)
                {
                    ModelState.AddModelError(nameof(request.Name), "Já existe uam categoria com este nome!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var category = await _categoryService.GetByIdAsync(id);
                if(category is null) return BadRequest();
                category.Name = request.Name;
                var result = await _categoryService.UpdateAsync(category);
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
                   "Erro ao alterar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if(category is null) return NotFound();
                await _categoryService.DeleteAsync(category);
                return NoContent();
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
        [Route("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);
                if (result == null) return NotFound();
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
                   "Erro ao buscar por nome"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> GetByname(string name)
        {
            try
            {
                var result = await _categoryService.GetByNameAsync(name);
                if (result == null) return NotFound();
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
                   "Erro ao buscar por id"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _categoryService.GetAllAsync();
                if (!result.Any()) return NotFound();
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
                   "Erro ao buscar por nome"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        [HttpPatch]
        [Route("deactivate/{id:int}")]
        public async Task<ActionResult> Deactivate(int id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);
                if (result == null) return NotFound();
                result.Active = false;
                await _categoryService.UpdateAsync(result);
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
                   "Erro ao buscar desativar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPatch]
        [Route("activate/{id:int}")]
        public async Task<ActionResult> Activate(int id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);
                if (result == null) return NotFound();
                result.Active = true;
                await _categoryService.UpdateAsync(result);
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
                   "Erro ao ativar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}