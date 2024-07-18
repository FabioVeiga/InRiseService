using System.Text;
using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.ComputerDto;
using InRiseService.Application.DTOs.CoolerDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Computers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputerController : ControllerBase
    {
        private readonly ILogger<ComputerController> _logger;
        private readonly IMapper _mapper;
        private readonly IComputerService _computerService;
        private readonly IImageService _imageService;
        private readonly ICoolerService _coolerService;
        private readonly IMotherBoardService _motherBoardService;
        private readonly ITowerService _towerService;
        private readonly IMemoryRamService _memoryRamService;
        private readonly IMemoryRomService _memoryRomService;
        private readonly IVideoBoardService _videoBoardService;
        private readonly IPowerSupplyService _powerSupplyService;
        private readonly IMonitorScreenService _monitorScreenService;
        private readonly IProcessorService _processorService;

        public ComputerController(
            ILogger<ComputerController> logger,
            IMapper mapper,
            IComputerService computerService,
            IImageService imageService,
            ICoolerService coolerService,
            IMotherBoardService motherBoardService,
            ITowerService towerService,
            IMemoryRamService memoryRamService,
            IMemoryRomService memoryRomService,
            IVideoBoardService videoBoardService,
            IPowerSupplyService powerSupplyService,
            IMonitorScreenService monitorScreenService,
            IProcessorService processorService)
        {
            _logger = logger;
            _mapper = mapper;
            _computerService = computerService;
            _imageService = imageService;
            _coolerService = coolerService;
            _motherBoardService = motherBoardService;
            _towerService = towerService;
            _memoryRamService = memoryRamService;
            _memoryRomService = memoryRomService;
            _videoBoardService = videoBoardService;
            _powerSupplyService = powerSupplyService;
            _monitorScreenService = monitorScreenService;
            _processorService = processorService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ComputerRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<Computer>(request);
                await Validate(request);
                if (!ModelState.IsValid) return BadRequest(new ValidationProblemDetails(ModelState));
                var result = await _computerService.InsertAsync(mapped);
                var mappedResponse = _mapper.Map<Computer>(result);

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
        public async Task<IActionResult> Update([FromBody] ComputerRequestDto request, int id)
        {
            try
            {
                var model = await _computerService.GetByIdAsync(id);
                if (model is null) return NotFound();
                if (!ModelState.IsValid) return BadRequest();
                await Validate(request);
                if (!ModelState.IsValid) return BadRequest(new ValidationProblemDetails(ModelState));
                model = _mapper.Map<Computer>(request);
                model.Id = id;
                await _computerService.UpdateAsync(model);
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
                var model = await _computerService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = true;
                await _computerService.UpdateAsync(model);
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
                var model = await _computerService.GetByIdAsync(id);
                if (model is null) return NotFound();
                model.Active = false;
                await _computerService.UpdateAsync(model);
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
                var result = await _computerService.GetByIdAsync(id);
                if (result == null) return NotFound();

                var mappedResponse = _mapper.Map<ComputerResponseDto>(result);
                mappedResponse.Images = await _imageService.GetByComputerIdAsync(result.Id);

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
                var result = await _computerService.GetByIdAsync(id);
                if (result == null) return NotFound();

                await _computerService.DeleteAsync(result);
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
        public async Task<IActionResult> GetFiltered([FromBody] ComputerFilterDto request)
        {
            try
            {
                var result = await _computerService.GetByFilterAsync(request);
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

        private async Task Validate(ComputerRequestDto request)
        {
            if(await _coolerService.GetByIdAsync(request.CoolerId) is null) ModelState.AddModelError(nameof(request.CoolerId), "Cooler não encontrada");
            if(await _motherBoardService.GetByIdAsync(request.MotherBoardId) is null) ModelState.AddModelError(nameof(request.MotherBoardId), "Placa Mãe não encontrada");
            if(await _towerService.GetByIdAsync(request.TowerId) is null) ModelState.AddModelError(nameof(request.TowerId), "Gabinete não encontrada");
            if(await _memoryRamService.GetByIdAsync(request.MemoryRamSlot01Id) is null)  ModelState.AddModelError(nameof(request.MemoryRamSlot01Id), "MemoryRamSlot01Id não encontrada");
            if(await _memoryRamService.GetByIdAsync(request.MemoryRamSlot02Id) is null) ModelState.AddModelError(nameof(request.MemoryRamSlot02Id), "MemoryRamSlot02Id não encontrada");
            if(await _memoryRomService.GetByIdAsync(request.MemoryRomHHDId) is null) ModelState.AddModelError(nameof(request.MemoryRomHHDId), "MemoryRomHHDId não encontrada");
            if(await _memoryRomService.GetByIdAsync(request.MemoryRomSSDId) is null) ModelState.AddModelError(nameof(request.MemoryRomSSDId), "MemoryRomSSDId não encontrada");
            if(await _videoBoardService.GetByIdAsync(request.VideoBoardId) is null) ModelState.AddModelError(nameof(request.VideoBoardId), "VideoBoardId não encontrada");
            if(await _powerSupplyService.GetByIdAsync(request.PowerSupplyId) is null) ModelState.AddModelError(nameof(request.PowerSupplyId), "PowerSupplyId não encontrada");
            if(await _monitorScreenService.GetByIdAsync(request.MonitorScreenId) is null) ModelState.AddModelError(nameof(request.MonitorScreenId), "MonitorScreenId não encontrada");
            if(await _processorService.GetByIdAsync(request.ProcessadorId) is null) ModelState.AddModelError(nameof(request.ProcessadorId), "ProcessadorId não encontrada");
        } 
    }
}