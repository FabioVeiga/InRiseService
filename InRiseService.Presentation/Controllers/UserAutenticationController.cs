using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.UserAutenticationDto;
using InRiseService.Application.Interfaces;
using InRiseService.Util;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAutenticationController : ControllerBase
    {
        private readonly ILogger<UserAutenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly IUserAutenticationService _userAutenticationService;

        public UserAutenticationController(ILogger<UserAutenticationController> logger, IMapper mapper,
        IUserProfileService userProfileService, IUserService userService, IUserAutenticationService userAutenticationService)
        {
            _logger = logger;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _userService = userService;
            _userAutenticationService = userAutenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserAutenticationDtoRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            var getUser = await _userService.GetByEmailAsync(request.Email);
            if(getUser is null)
            {
                ModelState.AddModelError(nameof(request.Email), "Não existe");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var checkProfile = _userProfileService.GetById((int)request.Profile);
            if(checkProfile is null)
            {
                ModelState.AddModelError(nameof(request.Profile), "Não existe");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }else{
                if((int)getUser.Profile != (int)request.Profile){
                    ModelState.AddModelError(nameof(request.Profile), "Perfil não condizente  com o usuário informado.");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }

            var checkPassword = PasswordHelper.CheckPassword(request.Password,getUser.Password);
            if(checkPassword == false)
            {
                ModelState.AddModelError(nameof(request.Password), "Não conferem! Tente novamente.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var acessTokenResult = _userAutenticationService.GetTokenAsync(request);
            var mappedResponse = _mapper.Map<UserAutenticationDtoResponse>(getUser);
            mappedResponse.AcessToken = acessTokenResult;

            var response = new ApiResponse<dynamic>(
                StatusCodes.Status200OK,
                mappedResponse
            );
            return Ok(response);
        }

    }
}