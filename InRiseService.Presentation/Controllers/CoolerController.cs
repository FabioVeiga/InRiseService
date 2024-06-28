using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.CoolerDto;
using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.Prices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoolerController : ControllerBase
    {
        private readonly ILogger<CoolerController> _logger;
        private readonly IMapper _mapper;
        private readonly ICoolerService _coolerService;
        private readonly IImageService _imageService;

        public CoolerController(
            ILogger<CoolerController> logger,
            IMapper mapper,
            ICoolerService coolerService,
            IImageService imageService,
            IBlobFileAzureService blobFileAzureService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _coolerService = coolerService;
            _imageService = imageService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CoolerInsertDto request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<Cooler>(request);
                mapped.Price = _mapper.Map<Price>(request.Price);
                var result = await _coolerService.InsertAsync(mapped);
                var mappedResponse = _mapper.Map<CoolerResponseDto>(result);
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
                   "Erro ao inserir"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] CoolerInsertDto request, int id)
        {
            try
            {
                var model = await _coolerService.GetByIdAsync(id);
                if (model is null) return NotFound();
                if (!ModelState.IsValid) return BadRequest();
                model.Name = request.Name;
                model.Air = request.Air;
                model.Refrigeration = request.Refrigeration;
                model.FanQuantity = request.FanQuantity;
                model.FanDiametric = request.FanDiametric;
                model.MaxVelocit = request.MaxVelocit;
                model.Dimension = request.Dimension;
                model.Price = _mapper.Map<Price>(request.Price);
                model.Price.Id = model.PriceId;
                await _coolerService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao alterar"
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
                var Cooler = await _coolerService.GetByIdAsync(id);
                if (Cooler is null) return NotFound();
                Cooler.Active = true;
                await _coolerService.UpdateAsync(Cooler);
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
                var Cooler = await _coolerService.GetByIdAsync(id);
                if (Cooler is null) return NotFound();
                Cooler.Active = false;
                await _coolerService.UpdateAsync(Cooler);
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

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _coolerService.GetByIdAsync(id);
                if (result == null) return NotFound();

                var mappedResponse = _mapper.Map<CoolerResponseDto>(result);
                mappedResponse.Images = await _imageService.GetByCoolerIdAsync(result.Id);
                mappedResponse.Price = _mapper.Map<PriceResponseDto>(result.Price);

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    mappedResponse
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}", ex);
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
                var result = await _coolerService.GetByIdAsync(id);
                if (result == null) return NotFound();

                await _coolerService.DeleteAsync(result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao deletar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFiltered([FromBody] CoolerFilterDto request)
        {
            try
            {
                var result = await _coolerService.GetByFilterAsync(request);
                if (result.TotalItems == 0)
                    return NotFound();

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
                   "Erro ao buscar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        
        

    }
}