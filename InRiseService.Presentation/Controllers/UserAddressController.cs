using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.UserAddressDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.UsersAddress;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAddressController : ControllerBase
    {
        private readonly ILogger<UserAddressController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserAddressService _userAddressService;

        public UserAddressController(ILogger<UserAddressController> logger, 
        IMapper mapper,
        IUserService userService,
        IUserAddressService userAddressService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _userAddressService = userAddressService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserAddressDtoInsertRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            var getUser = await _userService.GetByIdAsync(request.UserId);
            if(getUser is null)
            {
                ModelState.AddModelError(nameof(request.UserId), "Não existe");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            var mapped = _mapper.Map<UserAddress>(request);
            var responseMapped = await _userAddressService.InsertAsync(mapped);

            var response = new ApiResponse<dynamic>(
                StatusCodes.Status200OK,
                responseMapped
            );
            return Ok(response);
        }

    }
}