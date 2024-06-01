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
        [Route("get-by-name/{name}")]
        //[Authorize(Roles =  "Admin")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var result = _userProfileService.GetProfileByName(name);
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
        //[Authorize(Roles =  "Admin")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _userProfileService.GetProfileById(id);
                
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
        [Authorize(Roles =  "Admin")]
        public IActionResult GetAllProfile()
        {
            try
            {
                var result = _userProfileService.GetAllProfile();
                
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
                throw;
            }
        }
    }
}