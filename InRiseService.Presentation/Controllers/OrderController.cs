using System.Security.Claims;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.OrderDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Enums;
using InRiseService.Domain.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderStatusService _orderStatusService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICoolerService _coolerService;
        private readonly IMemoryRamService _memoryRamService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryRomService _memoryRomService;
        private readonly IMonitorScreenService _monitorScreenService;
        private readonly IMotherBoardService _motherBoardService;
        private readonly IPowerSupplyService _powerSupplyService;
        private readonly IProcessorService _processorService;
        private readonly ITowerService _towerService;
        private readonly IVideoBoardService _videoBoardService;
        private readonly ISendGridService _sendGridService;

        public OrderController(ILogger<OrderController> logger, IUserService userService, IOrderStatusService orderStatusService, IOrderService orderService, ICoolerService coolerService
        , IMemoryRamService memoryRamService, IHttpContextAccessor httpContextAccessor, IMemoryRomService memoryRomService, IMonitorScreenService monitorScreenService, IMotherBoardService motherBoardService
        , IPowerSupplyService powerSupplyService, IProcessorService processorService, ITowerService towerService, IVideoBoardService videoBoardService, ISendGridService sendGridService)
        {
            _logger = logger;
            _userService = userService;
            _orderStatusService = orderStatusService;
            _orderService = orderService;
            _coolerService = coolerService;
            _memoryRamService = memoryRamService;
            _httpContextAccessor = httpContextAccessor;
            _memoryRomService = memoryRomService;
            _monitorScreenService = monitorScreenService;
            _motherBoardService = motherBoardService;
            _powerSupplyService = powerSupplyService;
            _processorService = processorService;
            _towerService = towerService;
            _videoBoardService = videoBoardService;
            _sendGridService = sendGridService;
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
                var user = await _userService.GetByIdAsync(request.Userid);
                await _orderService.CreateItemsAsync(model.Id, request.ProductDtoRequests);
                await _orderService.CreateHistoricAsync(model.Id, model.OrderStatusId);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var dic = new Dictionary<string, string>(){
                        { "name", user.Name },
                        { "number", model.Number.ToString() }
                    };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                await _sendGridService.SendByTemplateAsync(model.User.Email, "A saga do seu novo PC começou!", dic, "d-ae07f00bcea74a9dad4cfbe0944a954f");

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _orderService.GetOrdersById(id);
                if (result == null) return NotFound();
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
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

        [HttpGet]
        [Route("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var result = await _orderService.GetOrdersByUserId(userId);
                if (result == null) return NotFound();
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
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

        [HttpPut]
        [Route("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] int statusId)
        {
            try
            {
                var model = await _orderService.GetOrdersById(id);
                if (model == null) return NotFound();
                var modelStatus = await _orderStatusService.GetByIdAsync(statusId);
                if (modelStatus == null) return NotFound();
                model.StatusId = statusId;
                var result = await _orderService.UpdateAsync(id, statusId);
                if (!result) return BadRequest();
                if (modelStatus.IsVisibleToUser)
                {
                    await _orderService.CreateHistoricAsync(id, statusId);
                }
                if(modelStatus.IsSendEmail)
                {
                    var dic = new Dictionary<string, string>(){
                        { "name", model.UserName },
                        { "number", model.Number.ToString() }
                    };
                    if(modelStatus.Name == "Pagamento Confirmado")
                        await _sendGridService.SendByTemplateAsync(model.UserEmail, "A tua aventura começa agora! Pedido confirmado.", dic, "d-2e48645929984f3eae7d4e1209529f26");
                    
                    if(modelStatus.Name == "Montagem" && model.DateDelivered.HasValue)
                    {
                        dic.Add("dateestimativedelivery",model.DateDelivered.Value.ToString());
                        await _sendGridService.SendByTemplateAsync(model.UserEmail, "Boas notícias! Estamos a montar o teu PC", dic, "d-209b2be3c25c4d0bb83ef70100e96eb0");
                    }
                    if(modelStatus.Name == "Distruibuição")
                        await _sendGridService.SendByTemplateAsync(model.UserEmail, "Está mais perto! O teu pedido foi expedido.", dic, "d-69690eaeb89f46369f50a5ad8d862521");
                    if(modelStatus.Name == "Concluído")
                        await _sendGridService.SendByTemplateAsync(model.UserEmail, "Missão cumprida! A tua aventura com o PC InRise começou.", dic, "d-08b751d7cbf74e5abfc91a03e08ef6b0");
                }
                return Ok();
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

        [HttpGet]
        [Route("historic-by-orderid/{orderId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetByHistoricOrderId(int orderId)
        {
            try
            {
                var result = await _orderService.GetOrderHistoricByOrderId(orderId);
                if (result == null) return NotFound();
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
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

        [HttpGet]
        [Route("historic-by-number/{number}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetByHistoricNumber(int number)
        {
            try
            {
                var result = await _orderService.GetOrderHistoricByNumber(number);
                if (result == null) return NotFound();
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
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
                    case EnumTypeCategoryImage.memoryRom:
                        if (await _memoryRomService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.memoryRom), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.monitorScreen:
                        if (await _monitorScreenService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.monitorScreen), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.motherBoard:
                        if (await _motherBoardService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.motherBoard), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.powerSupply:
                        if (await _powerSupplyService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.powerSupply), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.processor:
                        if (await _processorService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.processor), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.tower:
                        if (await _towerService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.tower), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    case EnumTypeCategoryImage.videoBoard:
                        if (await _videoBoardService.GetByIdAsync(item.ProductId) is null) ModelState.AddModelError(nameof(EnumTypeCategoryImage.videoBoard), $"Produto não encontrado - Id: ${item.ProductId}");
                        break;
                    default:
                        ModelState.AddModelError(nameof(item.TypeCategory), "Tipo {item.TypeCategory} de produto não encontrado!");
                        break;
                }
            }
        }
    }
}