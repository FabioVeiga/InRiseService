using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidationCodeController : ControllerBase
    {
        private readonly ILogger<ValidationCodeController> _logger;
        private readonly ITypeCodeValidationService _typeCodeValidationService;

        public ValidationCodeController(ILogger<ValidationCodeController> logger, ITypeCodeValidationService typeCodeValidationService)
        {
            _logger = logger;
            _typeCodeValidationService = typeCodeValidationService;
        }

        [HttpGet]
        [Route("get-by-name/{name}")]
        //[Authorize(Roles =  "Admin, User")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var result = _typeCodeValidationService.GetByName(name);
                if(result is null)
                    return NotFound();

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        //[Authorize(Roles =  "Admin, User")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _typeCodeValidationService.GetById(id);
                
                if(result is null)
                    return NotFound();

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        //[Authorize(Roles =  "Admin, User")]
        public IActionResult GetAllTypeValidationCode()
        {
            try
            {
                var result = _typeCodeValidationService.GetAll();
                
                if(result is null)
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
                    "Erro ao buscar todos!"
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}