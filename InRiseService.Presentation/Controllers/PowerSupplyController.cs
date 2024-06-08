using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.PowerSupplies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerSupplyController : ControllerBase
    {
        private readonly ILogger<PowerSupplyController> _logger;
        private readonly IMapper _mapper;
        private readonly IPowerSupplyService _powerSupplyService;

        public PowerSupplyController(
            ILogger<PowerSupplyController> logger,
            IMapper mapper,
            IPowerSupplyService powerSupplyService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _powerSupplyService = powerSupplyService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PowerSupplyInsertDto request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<PowerSupply>(request);
                var result = await _powerSupplyService.InsertAsync(mapped);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] PowerSupplyInsertDto request, int id)
        {
            try
            {
                var PowerSupply = await _powerSupplyService.GetByIdAsync(id);
                if(PowerSupply is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                PowerSupply.Name = request.Name;
                PowerSupply.Potency = request.Potency;
                PowerSupply.PotencyReal = request.PotencyReal;
                PowerSupply.Stamp = request.Stamp;
                PowerSupply.Modular = request.Modular;
                await _powerSupplyService.UpdateAsync(PowerSupply);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _powerSupplyService.GetByIdAsync(id);
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

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _powerSupplyService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _powerSupplyService.DeleteAsync(result);
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
        public async Task<IActionResult> GetFiltered([FromBody] PowerSupplyFilterDto request)
        {
            try
            {
               var result = await _powerSupplyService.GetByFilterAsync(request);
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