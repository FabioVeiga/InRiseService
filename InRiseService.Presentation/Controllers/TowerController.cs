using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Towers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InRiseService.Application.DTOs.TowerDto;
using InRiseService.Domain.Prices;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TowerController : ControllerBase
    {
        private readonly ILogger<TowerController> _logger;
        private readonly IMapper _mapper;
        private readonly ITowerService _towerService;
        private readonly IImageService _imageService;

        public TowerController(
            ILogger<TowerController> logger,
            IMapper mapper,
            ITowerService processorService,
            IImageService imageService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _towerService = processorService;
            _imageService = imageService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TowerDtoRequest request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<Tower>(request);
                var result = await _towerService.InsertAsync(mapped);
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
        public async Task<IActionResult> Update([FromBody] TowerDtoRequest request, int id)
        {
            try
            {
                var model = await _towerService.GetByIdAsync(id);
                if(model is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                var modelPrice = model.Price;
                model = _mapper.Map<Tower>(request);
                model.Id = id;
                model.Price = _mapper.Map<Price>(request.Price);
                model.Price.Id = modelPrice.Id;
                model.PriceId = modelPrice.Id;
                await _towerService.UpdateAsync(model);
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
                var result = await _towerService.GetByIdAsync(id);
                if(result == null) return NotFound();

                var mappedResponse = _mapper.Map<TowerDtoResponse>(result);
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
                var result = await _towerService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _towerService.DeleteAsync(result);
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
        public async Task<IActionResult> GetFiltered([FromQuery] TowerFilterDto request)
        {
            try
            {
               var result = await _towerService.GetByFilterAsync(request);
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
                var model = await _towerService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = true;
                await _towerService.UpdateAsync(model);
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
                var model = await _towerService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = false;
                await _towerService.UpdateAsync(model);
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