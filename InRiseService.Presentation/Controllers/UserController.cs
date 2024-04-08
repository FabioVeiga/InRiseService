using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.UserDto;
using InRiseService.Application.Interfaces;
using InRiseService.Application.UserDto;
using InRiseService.Domain.Users;
using InRiseService.Util;
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDtoUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await _userService.GetByIdAsync(request.Id);
                if (userId is null)
                {
                    ModelState.AddModelError(nameof(request.Id), "Não existe!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                if (!userId.Active)
                {
                    ModelState.AddModelError(nameof(userId.Active), "Usuário desativado");
                    if(userId.DeleteIn is not null)
                        ModelState.AddModelError(nameof(userId.DeleteIn), "Usuário deletado");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                var checkEmail = await _userService.CheckEmailIfExists(request.Email);
                if (checkEmail is not null && request.Id != checkEmail.Id)
                {
                    ModelState.AddModelError(nameof(request.Email), "Já cadastrado.");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                else
                {
                    if(userId.Email != request.Email)
                    {
                        userId.EmailValide = false;
                    }
                }

                if(userId.PhoneNumber != request.PhoneNumber) 
                { 
                    userId.PhoneNumberValide = false;
                }

                if (request.Password is null)
                    userId.Password = userId.Password;
                else
                    userId.Password = PasswordHelper.EncryptPassword(request.Password);

                userId.Name= request.Name;
                userId.Email = request.Email;
                userId.Lastname= request.Lastname;
                userId.PhoneNumber = request.PhoneNumber;

                var result = await _userService.UpdateAsync(userId);
                var mappedResponse = _mapper.Map<UserDtoResponse>(result);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status202Accepted,
                    mappedResponse
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao alterar o usuário!"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await _userService.GetByIdAsync(id);
                if (userId is null)
                {
                    ModelState.AddModelError(nameof(id), "Não existe!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                var result = await _userService.DeleteAsync(userId);
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
                   "Erro ao deletar usuário!"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}