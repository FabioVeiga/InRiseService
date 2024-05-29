using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.MotherBoards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotherBoardController : ControllerBase
    {
        private readonly ILogger<MotherBoardController> _logger;
        private readonly IMapper _mapper;
        private readonly IMotherBoardService _motherBoardService;

        public MotherBoardController(
            ILogger<MotherBoardController> logger,
            IMapper mapper,
            IMotherBoardService motherBoardService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _motherBoardService = motherBoardService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] MotherBoardDtoInsertRequest request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<MotherBoard>(request);
                var result = await _motherBoardService.InsertAsync(mapped);
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
        public async Task<IActionResult> Update([FromBody] MotherBoardDtoInsertRequest request, int id)
        {
            try
            {
                var motherBoard = await _motherBoardService.GetByIdAsync(id);
                if(motherBoard is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                motherBoard.Name = request.Name;
                motherBoard.Socket = request.Socket;
                motherBoard.Potency = request.Potency;
                motherBoard.SocketM2 = request.SocketM2;
                motherBoard.SocketMemory = request.SocketMemory;
                motherBoard.SocketMemoryVideo = request.SocketMemoryVideo;
                motherBoard.SocketSSD = request.SocketSSD;
                await _motherBoardService.UpdateAsync(motherBoard);
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
                var result = await _motherBoardService.GetByIdAsync(id);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFiltered([FromBody] MotherBoardDtoFilterRequest request)
        {
            try
            {
               var result = await _motherBoardService.GetByFilterAsync(request);
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