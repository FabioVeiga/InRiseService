using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.ValidationCodeDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidationCodeController : ControllerBase
    {
        private readonly ILogger<ValidationCodeController> _logger;
        private readonly ITypeCodeValidationService _typeCodeValidationService;
        private readonly IUserService _userService;
        private readonly IValidationCodeService _validationCodeService;
        private readonly ISendGridService _sendGridService;

        public ValidationCodeController(
            ILogger<ValidationCodeController> logger, 
            ITypeCodeValidationService typeCodeValidationService,
            IUserService userService,
            IValidationCodeService validationCodeService,
            ISendGridService sendGridService)
        {
            _logger = logger;
            _typeCodeValidationService = typeCodeValidationService;
            _userService = userService;
            _validationCodeService = validationCodeService;
            _sendGridService = sendGridService;
        }

        [HttpPost]
        [Route("generate-by-email")]
        [AllowAnonymous]
        public  async Task<ActionResult> Generate([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);
                if(user is null) return NotFound();

                var dic = new Dictionary<string, string>(){
                        { "name", user.Name }
                    };

                var code = await _validationCodeService.GetLastValideCodeByUserIdAsync(user.Id);
                if(code is not null)
                {
                    dic.Add("code", code.Code.ToString());
                    dic.Add("dateexpiration", code.ExpirateAt.ToString("dd/MM/yyyy hh:mm"));
                    await _sendGridService.SendByTemplateAsync(user.Email, "Confirmação de conta", dic, "d-5044b14761b343b4a7c085978ca38fb4");

                    return BadRequest(
                        new ApiResponse<dynamic>(
                        StatusCodes.Status400BadRequest,
                        $"O código de validação ainda está válido. E reenviado código por email!"
                    ));
                }

                var newCode = await _validationCodeService.InsertAsync(user.Id);
                dic.Add("code", newCode.Code.ToString());
                dic.Add("dateexpiration", newCode.ExpirateAt.ToString("dd/MM/yyyy hh:mm"));
                
                await _sendGridService.SendByTemplateAsync(user.Email, "Confirmação de conta", dic, "d-5044b14761b343b4a7c085978ca38fb4");

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status201Created,
                    $"O código de validação gerado e enviado para o email!"
                );
                return Ok(response);
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
        [Route("validate-by-email")]
        public async Task<ActionResult> Validate([FromBody] ValidateCodeByEmailRequestDto requestDto)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var code = await _validationCodeService.GetLastValideCodeByCode(requestDto.Code);
                if(code is null) 
                return BadRequest(
                    new ApiResponse<dynamic>(
                    StatusCodes.Status400BadRequest,
                    $"Código de validação não existe!"
                ));

                if(code.User?.Email != requestDto.Email)
                return BadRequest(
                    new ApiResponse<dynamic>(
                    StatusCodes.Status400BadRequest,
                    $"Email não existe! Ou o código pertence a outra conta."
                ));

                if(code.TypeCode != Domain.Enums.EnumTypeCodeValidation.Email)
                return BadRequest(
                    new ApiResponse<dynamic>(
                    StatusCodes.Status400BadRequest,
                    $"Código não é para este tipo"
                ));

                if(code.Code != requestDto.Code)
                return BadRequest(
                    new ApiResponse<dynamic>(
                    StatusCodes.Status400BadRequest,
                    $"Código não confere"
                ));

                code.IsValidate =  true;
                await _validationCodeService.UpdateAsync(code);

                code.User.EmailValide = true;
                await _userService.UpdateAsync(code.User);

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    $"O código foi validado!"
                );
                return Ok(response);
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
        [Route("get-by-name/{name}")]
        [Authorize(Roles =  "Admin, User")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var result = _typeCodeValidationService.GetByName(name);
                if(result is null)
                    return NotFound();

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        [Authorize(Roles =  "Admin, User")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _typeCodeValidationService.GetById(id);
                
                if(result is null)
                    return NotFound();

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles =  "Admin, User")]
        public IActionResult GetAllTypeValidationCode()
        {
            try
            {
                var result = _typeCodeValidationService.GetAll();
                
                if(result is null)
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
                    "Erro ao buscar todos!"
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}