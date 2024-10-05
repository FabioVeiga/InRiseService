using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.LandingPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LandingPageController : ControllerBase
    {
        private readonly ILogger<LandingPageController> _logger;
        private readonly ILandingPageService _service;
        private readonly ISendGridService _sendGridService;
        private readonly string _secret = "naf9uafjh_+mcdsaIFD023";

        public LandingPageController(
            ILogger<LandingPageController> logger,
            ILandingPageService service,ISendGridService sendGridService)
        {
            _logger = logger;
            _service = service;
            _sendGridService = sendGridService;
        }

        [HttpPost]
        [AllowAnonymous]
        public  async Task<ActionResult> Insert([FromHeader] string secret, [FromBody] LandingPage request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                if(secret  != _secret) return Unauthorized();
                var model = await _service.InsertAsync(request);
                var dic = new Dictionary<string, string>(){
                    { "name", model.Name }
                };
                var sendEmail = await _sendGridService.SendByTemplateAsync(model.Name, "LandPage.", dic, "d-2e48645929984f3eae7d4e1209529fXX");
                if(sendEmail)
                    model.IsSendEmail = true;
                else
                    model.IsSendEmail = false;

                await _service.UpdateAsync(model);
                return Ok();
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
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public  async Task<ActionResult> SendEmail([FromBody] LandingPage request)
        {
            try
            {
                var dic = new Dictionary<string, string>(){
                    { "name", request.Name }
                };
                var sendEmail = await _sendGridService.SendByTemplateAsync(request.Name, "LandPage.", dic, "d-2e48645929984f3eae7d4e1209529fXX");
                if(sendEmail)
                    request.IsSendEmail = true;
                else
                    request.IsSendEmail = false;

                await _service.UpdateAsync(request);
                return Ok();
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

        [HttpGet]
        [AllowAnonymous]
        public  async Task<ActionResult> GetAll([FromHeader] string secret)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                if(secret  != _secret) return Unauthorized();
                var lista = await _service.GetAll();
                return Ok(lista);
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