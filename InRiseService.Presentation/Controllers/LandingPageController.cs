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
                var isEmailInserted = await _service.GetByEmailAsync(request.Email);
                if (isEmailInserted is not null) return BadRequest("Email already exists");
                var model = await _service.InsertAsync(request);
                var dic = new Dictionary<string, string>(){
                    { "name", model.Name }
                };
                var sendEmail = await _sendGridService.SendByTemplateAsync(model.Email, "teste", dic, "d-fcc470eb9b6b4d7aa3a640cbc0fb54b3");
                if(sendEmail)
                    model.IsSendEmail = true;
                else
                    model.IsSendEmail = false;

                await _service.UpdateAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao inserir!"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SendEmail([FromBody] string email)
        {
            try
            {
                var model = await _service.GetByEmailAsync(email);
                if (model is null) return NotFound();
                var dic = new Dictionary<string, string>(){
                    { "name", model.Name }
                };
                var sendEmail = await _sendGridService.SendByTemplateAsync(model.Name, "teste", dic, "d-fcc470eb9b6b4d7aa3a640cbc0fb54b3");
                if(sendEmail)
                    model.IsSendEmail = true;
                else
                    model.IsSendEmail = false;

                await _service.UpdateAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao enviar email!"
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
                   "Erro ao buscar todos!"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
       
    }

    
}