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
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly IUserAutenticationService _userAutenticationService;

        public UserAutenticationController(ILogger<UserAutenticationController> logger, IUserProfileService userProfileService, IUserService userService, IUserAutenticationService userAutenticationService)
        {
            _logger = logger;
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

            var checkProfile = _userProfileService.GetProfileById((int)request.Profile);
            if(checkProfile is null)
            {
                ModelState.AddModelError(nameof(request.Profile), "Não existe");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var checkPassword = PasswordHelper.CheckPassword(request.Password,getUser.Password);
            if(checkPassword == false)
            {
                ModelState.AddModelError(nameof(request.Password), "Não conferem! Tente novamente.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            return Ok();
        }

    }
}