using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.PaymentDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetById([FromBody] PaymentProductsRequestDto paymentProductsRequestDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _paymentService.CreateSessionStripe(paymentProductsRequestDto);

                if (result is null)
                    return NotFound();

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
    
    }
}