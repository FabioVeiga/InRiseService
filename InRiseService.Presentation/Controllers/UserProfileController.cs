using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(ILogger<UserProfileController> logger, IUserProfileService userProfileService)
        {
            _logger = logger;
            _userProfileService = userProfileService;
        }

        [HttpGet]
        [Route("GetByName")]
        [Authorize(Roles =  "Admin")]
        public IActionResult GetByName([FromQuery] string name)
        {
            try
            {
                var result = _userProfileService.GetProfileByName(name);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                if(result is null)
                {
                    response.Status = StatusCodes.Status404NotFound;
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpGet]
        [Route("GetById")]
        [Authorize(Roles =  "Admin")]
        public IActionResult GetById([FromQuery]int id)
        {
            try
            {
                var result = _userProfileService.GetProfileById(id);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                if(result is null)
                {
                    response.Status = StatusCodes.Status404NotFound;
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllProfile")]
        [Authorize(Roles =  "Admin")]
        public IActionResult GetAllProfile()
        {
            try
            {
                var result = _userProfileService.GetAllProfile();
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                if(result is null)
                {
                    response.Status = StatusCodes.Status404NotFound;
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}