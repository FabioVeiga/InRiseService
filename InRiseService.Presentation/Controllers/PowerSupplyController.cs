using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.PowerSupplyDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.PowerSupplies;
using InRiseService.Domain.Prices;
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
        private readonly IImageService _imageService;

        public PowerSupplyController(
            ILogger<PowerSupplyController> logger,
            IMapper mapper,
            IPowerSupplyService powerSupplyService,
            IImageService imageService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _powerSupplyService = powerSupplyService;
            _imageService = imageService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PowerSupplyDtoRequest request)
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
        public async Task<IActionResult> Update([FromBody] PowerSupplyDtoRequest request, int id)
        {
            try
            {
                var model = await _powerSupplyService.GetByIdAsync(id);
                if(model is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                model = _mapper.Map<PowerSupply>(request);
                model.Id = id;
                model.Price = _mapper.Map<Price>(request.Price);
                model.Price.Id = model.PriceId;
                await _powerSupplyService.UpdateAsync(model);
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

                var mappedResponse = _mapper.Map<PowerSupplyDtoResponse>(result);
                mappedResponse.Images = await _imageService.GetByPowerSupplyIdAsync(result.Id);
                mappedResponse.Price = _mapper.Map<PriceResponseDto>(result.Price);

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    mappedResponse
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
    
        [HttpPut]
        [Route("Activate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                var model = await _powerSupplyService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = true;
                await _powerSupplyService.UpdateAsync(model);
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
                var model = await _powerSupplyService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = false;
                await _powerSupplyService.UpdateAsync(model);
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