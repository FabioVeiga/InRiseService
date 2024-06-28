using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.DTOs.PriceDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.MemoriesRam;
using InRiseService.Domain.Prices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemoryRamController : ControllerBase
    {
        private readonly ILogger<MemoryRamController> _logger;
        private readonly IMapper _mapper;
        private readonly IMemoryRamService _memoryRamService;

        public MemoryRamController(
            ILogger<MemoryRamController> logger,
            IMapper mapper,
            IMemoryRamService pmemoryRamService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _memoryRamService = pmemoryRamService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] MemoryRamInsertDto request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<MemoryRam>(request);
                mapped.Price = _mapper.Map<Price>(request.Price);
                var result = await _memoryRamService.InsertAsync(mapped);
                var mappedResponse = _mapper.Map<MemoryRamResponseDto>(result);
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
        public async Task<IActionResult> Update([FromBody] MemoryRamInsertDto request, int id)
        {
            try
            {
                var memoryRam = await _memoryRamService.GetByIdAsync(id);
                if(memoryRam is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                memoryRam.Frequency = request.Frequency;
                memoryRam.Name = request.Name;
                memoryRam.Socket = request.Socket;
                memoryRam.Capacity = request.Capacity;
                await _memoryRamService.UpdateAsync(memoryRam);
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
                var result = await _memoryRamService.GetByIdAsync(id);
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
                var result = await _memoryRamService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _memoryRamService.DeleteAsync(result);
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
        public async Task<IActionResult> GetFiltered([FromBody] MemoryRamFilterDto request)
        {
            try
            {
               var result = await _memoryRamService.GetByFilterAsync(request);
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