using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.Interfaces;
using InRiseService.Application.UserDto;
using InRiseService.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUserProfileService userProfileService, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserDtoInsertRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                if(_userProfileService.GetProfileById((int)request.Profile) is null)
                {
                    ModelState.AddModelError(nameof(request.Profile), "Não existe!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var checkEmail = await _userService.CheckEmailIfExists(request.Email);
                if(checkEmail is not null)
                {
                    ModelState.AddModelError(nameof(request.Email), "Já cadastrado.");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var checkPhoneNumber = await _userService.CheckPhoneNumberIfExists(request.PhoneNumber);
                if(checkPhoneNumber is not null)
                {
                    ModelState.AddModelError(nameof(request.Email), "Já cadastrado.");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var mapped = _mapper.Map<User>(request);
                var result = await _userService.InsertAsync(mapped);
                var mappedResponse = _mapper.Map<UserDtoResponse>(result);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    mappedResponse
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                 var response = new ApiResponse<dynamic>(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao inserir usuário!"
                );
                return StatusCode(StatusCodes.Status500InternalServerError,response);
            }
        }
    }
}