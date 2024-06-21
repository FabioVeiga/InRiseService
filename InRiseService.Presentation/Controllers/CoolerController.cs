using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.MemoryRamDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.ImagesSite;
using InRiseService.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoolerController : ControllerBase
    {
        private readonly ILogger<CoolerController> _logger;
        private readonly IMapper _mapper;
        private readonly ICoolerService _coolerService;
        private readonly IImageService _imageService;
        private readonly IBlobFileAzureService _blobFileAzureService;

        public CoolerController(
            ILogger<CoolerController> logger,
            IMapper mapper,
            ICoolerService coolerService,
            IImageService imageService,
            IBlobFileAzureService blobFileAzureService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _coolerService = coolerService;
            _imageService = imageService;
            _blobFileAzureService = blobFileAzureService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CoolerInsertDto request)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var mapped = _mapper.Map<Cooler>(request);
                var result = await _coolerService.InsertAsync(mapped);
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
                   "Erro ao inserir"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] CoolerInsertDto request, int id)
        {
            try
            {
                var Cooler = await _coolerService.GetByIdAsync(id);
                if(Cooler is null) return NotFound();
                if(!ModelState.IsValid) return BadRequest();
                Cooler.Name = request.Name;
                Cooler.Air = request.Air;
                Cooler.Refrigeration = request.Refrigeration;
                Cooler.FanQuantity = request.FanQuantity;
                Cooler.FanDiametric = request.FanDiametric;
                Cooler.MaxVelocit = request.MaxVelocit;
                Cooler.Dimension = request.Dimension;
                await _coolerService.UpdateAsync(Cooler);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao alterar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _coolerService.GetByIdAsync(id);
                if(result == null) return NotFound();

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
                   "Erro ao buscar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _coolerService.GetByIdAsync(id);
                if(result == null) return NotFound();

                await _coolerService.DeleteAsync(result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao deletar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFiltered([FromBody] CoolerFilterDto request)
        {
            try
            {
               var result = await _coolerService.GetByFilterAsync(request);
                if(result.TotalItems == 0)
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
                   "Erro ao buscar"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    
        [HttpPost]
        [Route("upload-image/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, int id)
        {
            try
            {
                var result = await _coolerService.GetByIdAsync(id);
                if(result == null) return NotFound();

                var validation = FileHelper.ValidateImage(file);
                if(validation.Count > 0)
                {
                    foreach (var item in validation)
                    {
                        ModelState.AddModelError(nameof(file), item);
                    }
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                ImagensProduct model = new(){
                    CoolerId = result.Id,
                    ImageName = file.FileName,
                    Pathkey = $"cooler/{result.Id}"
                };

                Uri? urlImage = null;
                using (var stream = file.OpenReadStream())
                {
                    urlImage = await _blobFileAzureService.UploadFileAsync(stream,model.Pathkey,model.ImageName);
                    if(urlImage is null)
                    {
                        ModelState.AddModelError(nameof(file), "Erro no upload");
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    }
                }

                model = await _imageService.InsertAsync(model);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    urlImage
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao upload image"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    
    }
}