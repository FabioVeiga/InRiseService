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
        private readonly string _secret = "naf9uafjh_+mcdsaIFD023";

        public LandingPageController(
            ILogger<LandingPageController> logger,
            ILandingPageService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public  async Task<ActionResult> Insert([FromHeader] string secret, [FromBody] LandingPage request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                if(secret  != "naf9uafjh_+mcdsaIFD023") return Unauthorized();
                var model = await _service.InsertAsync(request);
                if(model is null) return BadRequest(); 
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
                if(secret  != "naf9uafjh_+mcdsaIFD023") return Unauthorized();
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