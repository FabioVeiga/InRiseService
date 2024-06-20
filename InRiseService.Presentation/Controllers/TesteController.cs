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
        private readonly IBlobFileAzureService _blobFileAzureService;
        private readonly string _secret = "inrise2024";

        public TesteController(
            ILogger<TesteController> logger,
            ISendGridService sendGridService,
            IBlobFileAzureService blobFileAzureService
            )
        {
            _logger = logger;
            _sendGridService = sendGridService;
            _blobFileAzureService = blobFileAzureService;
        }

        [HttpPost]
        [AllowAnonymous]
        public  async Task<ActionResult> Teste01([FromBody] string secret, string email)
        {
            try
            {
                if(secret  != "inrise2024") return Unauthorized();
                email = string.IsNullOrEmpty(email) ? "droidbinho@gmail.com" : email;
                var teste = await _sendGridService.SendAsync(email, "Teste envio", "Teste", "Este é um email de teste");
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

        [HttpPost]
        [AllowAnonymous]
        [Route("Teste02")]
        public  async Task<ActionResult> Teste02([FromBody] string secret, string templateId)
        {
            try
            {
                if(secret  != _secret) return Unauthorized();
                var dic = new Dictionary<string, string>(){
                    { "-name-", "Fabinho" },
                    { "-email-", "droidbinho@gmail.com" }
                };

                var teste = await _sendGridService.SendByTemplateAsync("droidbinho@gmail.com", "Teste Template", dic, templateId);
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

        [HttpPost]
        [AllowAnonymous]
        [Route("upload-image")]
        public  async Task<ActionResult> Teste03([FromQuery] string secret, [FromForm] DtoUploadFileTeste request)
        {
            try
            {
                if(secret  != _secret) return Unauthorized();
                if (request.File == null || request.File.Length == 0) return BadRequest();
                using (var stream = request.File.OpenReadStream())
                {
                    await _blobFileAzureService.UploadFileAsync(stream, request.File.FileName, request.Pathkey);
                }
                return Ok("Imagem enviada com sucesso!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class DtoUploadFileTeste{
            public IFormFile File { get; set; } = default!;
            public string Pathkey { get; set; } = default!;

        }
       
    }

    
}