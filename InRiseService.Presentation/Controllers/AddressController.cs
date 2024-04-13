using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Addressed;
using InRiseService.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _userProfileService;
        private readonly IZipCodeBaseService _zipCodeBaseService;
        private readonly IAddressService _addressService;

        public AddressController(
            IMapper mapper,
            ILogger<AddressController> logger, 
            IAddressService userProfileService, 
            IZipCodeBaseService zipCodeBaseService,
            IAddressService addressService)
        {
            _mapper = mapper;
            _logger = logger;
            _userProfileService = userProfileService;
            _zipCodeBaseService = zipCodeBaseService;
            _addressService = addressService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressByZipCode(string postalcode)
        {
            try
            {
                if(string.IsNullOrEmpty(postalcode)) return BadRequest();
                
                if(!StringHelper.CheckPostalCode(postalcode))
                {
                    ModelState.AddModelError(nameof(postalcode), "Invalido");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                var getAddress = await _addressService.GetByPostalCode(postalcode);
                if(getAddress is null)return NotFound();

                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    getAddress
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao buscar Postal Code!"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
                throw;
            }
        }

        /* [HttpGet]
        public async Task<IActionResult> GetAddressByZipCode(string postalcode)
        {
            try
            {
                if(string.IsNullOrEmpty(postalcode)) return BadRequest();
                if(!StringHelper.CheckPostalCode(postalcode))
                {
                    ModelState.AddModelError(nameof(postalcode), "Invalido");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var getAddress = await _addressService.GetByPostalCode(postalcode);
                if(getAddress is not null){
                    var responseAddress = new ApiResponse<dynamic>(
                        StatusCodes.Status200OK,
                        getAddress
                    );
                    return Ok(responseAddress);
                }
                var result = await _zipCodeBaseService.GetAddressByZipCode(postalcode);
                if(result == null) return BadRequest();
                var mapped = _mapper.Map<Address>(result);
                var inserted = await _addressService.InsertAsync(mapped);
                var response = new ApiResponse<dynamic>(
                        StatusCodes.Status200OK,
                        inserted
                    );
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
           
        } */
    }
}