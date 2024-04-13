using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.UserAddressDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Addressed;
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
        private readonly IAddressService _addressService;

        public UserAddressController(ILogger<UserAddressController> logger, 
        IMapper mapper,
        IUserService userService,
        IUserAddressService userAddressService,
        IAddressService addressService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _userAddressService = userAddressService;
            _addressService = addressService;
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
            if(request.IsDefault)
            {
                var getCurrent = await _userAddressService.GetCurrentDefaultAsync(request.UserId);
                if(getCurrent is not null)
                {
                    getCurrent.IsDefault = false;
                    await _userAddressService.UpdateAsync(getCurrent);
                }
            }

            var mapped = _mapper.Map<UserAddress>(request);
            var checkAddress = await _addressService.GetByPostalCode(request.PostalCode);
            if(checkAddress is null){
                var mappedCheckAddress = _mapper.Map<Address>(request);
                mappedCheckAddress = await _addressService.InsertAsync(mappedCheckAddress);
                mapped.AddressId = mappedCheckAddress.Id;
            }
            
            var responseMapped = await _userAddressService.InsertAsync(mapped);
            var response = new ApiResponse<dynamic>(
                StatusCodes.Status200OK,
                responseMapped
            );
            return Ok(response);
        }

    }
}