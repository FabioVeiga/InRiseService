using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICoolerService _coolerService;
        private readonly IMemoryRamService _memoryRamService;
        //private readonly IBlobFileAzureService _blobFileAzureService;
        //private readonly IMemoryRomService _memoryRomService;
        //private readonly IImageService _imageService;
        //private readonly IMonitorScreenService _monitorScreenService;
        //private readonly IMotherBoardService _motherBoardService;
        //private readonly IPowerSupplyService _powerSupplyService;
        //private readonly IProcessorService _processorService;
        //private readonly ITowerService _towerService;
        //private readonly IVideoBoardService _videoBoardService;
        //private readonly IComputerService _computerService;

        public OrderController(ILogger<OrderController> logger, IMapper mapper, IUserService userService, IOrderService orderService, ICoolerService coolerService
        ,IMemoryRamService memoryRamService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _orderService = orderService;
            _coolerService = coolerService;
            _memoryRamService = memoryRamService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] OrderDtoRequest request)
        {
            try
            {
                await Validate(request);
                if (!ModelState.IsValid) return BadRequest(new ValidationProblemDetails(ModelState));

                var model = await _orderService.CreateAsync(request.Userid, request.TotalPrice);
                await _orderService.CreateItemsAsync(model.Id, request.ProductDtoRequests);
                await _orderService.CreateHistoricAsync(model.Id, model.OrderStatusId);

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    model
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}", ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao inserir"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
               var result = await _orderService.GetOrdersById(id);
               if(result == null) return NotFound();
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

        private async Task Validate(OrderDtoRequest request)
        {
            if (request is null)
            {
                ModelState.AddModelError(nameof(request), "Request inválido!");
                return;
            }
            if (await _userService.GetByIdAsync(request.Userid) is null)
            {
                ModelState.AddModelError(nameof(request.Userid), "Usuário não encontrado!");
                return;
            }
            foreach (var item in request.ProductDtoRequests)
            {
                switch (item.TypeCategory)
                {
                    case EnumTypeCategoryImage.cooler:
                        if (await _coolerService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.cooler), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.memoryRam:
                        if (await _memoryRamService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.memoryRam), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    default:
                        ModelState.AddModelError(nameof(item.TypeCategory), "Tipo {item.TypeCategory} de produto não encontrado!");
                    break;
                }
            }
        }
    }
}