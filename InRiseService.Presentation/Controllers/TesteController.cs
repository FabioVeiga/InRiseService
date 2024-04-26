using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ILogger<TesteController> _logger;
        private readonly ISendGridService _sendGridService;

        public TesteController(
            ILogger<TesteController> logger,
            ISendGridService sendGridService
            )
        {
            _logger = logger;
            _sendGridService = sendGridService;
        }

        [HttpPost]
        [AllowAnonymous]
        public  async Task<ActionResult> Teste01()
        {
            try
            {
                var teste = await _sendGridService.SendAsync("droidbinho@gmail.com", "Fabio", "Teste", "Este é um email de teste");
                return Ok(teste);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao gerar código de validação!"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

       
    }
}