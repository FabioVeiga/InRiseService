using AutoMapper;
using InRiseService.Application.DTOs.ApiResponseDto;
using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.Interfaces;
using InRiseService.Domain.ImagesSite;
using InRiseService.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InRiseService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IMapper _mapper;
        private readonly ICoolerService _coolerService;
        private readonly IMemoryRamService _memoryRamService;
        private readonly IBlobFileAzureService _blobFileAzureService;
        private readonly IMemoryRomService _memoryRomService;
        private readonly IImageService _imageService;
        private readonly IMonitorScreenService _monitorScreenService;
        private readonly IMotherBoardService _motherBoardService;
        private readonly IPowerSupplyService _powerSupplyService;
        private readonly IProcessorService _processorService;
        private readonly ITowerService _towerService;
        private readonly IVideoBoardService _videoBoardService;


        public ImageController(
            ILogger<ImageController> logger,
            IMapper mapper,
            ICoolerService coolerService,
            IImageService imageService,
            IBlobFileAzureService blobFileAzureService,
            IMemoryRamService memoryRamService,
            IMemoryRomService memoryRomService,
            IMonitorScreenService monitorScreenService,
            IMotherBoardService motherBoardService,
            IPowerSupplyService powerSupplyService,
            IProcessorService processorService,
            ITowerService towerService,
            IVideoBoardService videoBoardService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _coolerService = coolerService;
            _imageService = imageService;
            _blobFileAzureService = blobFileAzureService;
            _memoryRamService = memoryRamService;
            _memoryRomService = memoryRomService;
            _monitorScreenService = monitorScreenService;
            _motherBoardService = motherBoardService;
            _powerSupplyService = powerSupplyService;
            _processorService = processorService;
            _towerService = towerService;
            _videoBoardService = videoBoardService;
        }

        [HttpGet]
        [Route("get-all-category-image")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllCategoryImage()
        {
            var result = _imageService.GetImageCategories();
            var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
        }

        [HttpGet]
        [Route("get-category-image-name")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCategoryImageByName([FromQuery] string nameCategoryImage)
        {
            var result = _imageService.GetImageCategoryByName(nameCategoryImage);
            if(result is null) return NotFound();
            var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
        }

        [HttpGet]
        [Route("get-category-image-id/{idCategoryImage}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCategoryImageByName(int idCategoryImage)
        {
            var result = _imageService.GetImageCategoryById(idCategoryImage);
            if(result is null) return NotFound();
            var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    result
                );
                return Ok(response);
        }

        [HttpPost]
        [Route("upload/{nameCategoryImage}/{idProduct}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, string nameCategoryImage, int idProduct)
        {
            try
            {
                var validation = FileHelper.ValidateImage(file);
                if (validation.Count > 0)
                {
                    foreach (var item in validation)
                    {
                        ModelState.AddModelError(nameof(file), item);
                    }
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
                var category = _imageService.GetImageCategoryByName(nameCategoryImage);
                if (category is null)if (category is null){
                    ModelState.AddModelError(nameof(nameCategoryImage), "Categoria não encontrada");
                    return NotFound(new ValidationProblemDetails(ModelState));
                }
                var modelImage = GenerateModel(category, idProduct, file);
                var isProductExist = await ValidateProductIdByCategoryName(modelImage, category);
                if (!isProductExist){
                    ModelState.AddModelError(nameof(idProduct), "idProduct não encontrada");
                    return NotFound(new ValidationProblemDetails(ModelState));
                }
                
                Uri? urlImage = null;
                using (var stream = file.OpenReadStream())
                {
                    urlImage = await _blobFileAzureService.UploadFileAsync(stream, modelImage.Pathkey, modelImage.ImageName);
                    if (urlImage is null)
                    {
                        ModelState.AddModelError(nameof(file), "Erro no upload");
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    }
                }

                modelImage = await _imageService.InsertAsync(modelImage);
                var response = new ApiResponse<dynamic>(
                    StatusCodes.Status200OK,
                    urlImage
                );
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao upload image"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("delete/{idImage}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImage(int idImage)
        {
            try
            {
                var result = await _imageService.GetByIdAsync(idImage);
                if (result == null) return NotFound();

                var remove = await _blobFileAzureService.DeleteFileAsync($"{result.Pathkey}/{result.ImageName}");
                if (!remove)
                {
                    ModelState.AddModelError("Image", "Erro ao deletar imagem!");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                await _imageService.DeleteAsync(result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Ex}",ex);
                var response = new ApiResponse<dynamic>(
                   StatusCodes.Status500InternalServerError,
                   "Erro ao delete image"
               );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        private static ImagensProduct GenerateModel(ImageCategoryDto imageCategoryDto, int idProduct, IFormFile file)
        {
           var model = imageCategoryDto switch
            {
                { Id: 1 } => new ImagensProduct(){
                    CoolerId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 2 } => new ImagensProduct(){
                    MemoryRamId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 3 } => new ImagensProduct(){
                    MemoryRomId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 4 } => new ImagensProduct(){
                    MonitorScreenId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 5 } => new ImagensProduct(){
                    MotherBoardId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 6 } => new ImagensProduct(){
                    PowerSupplyId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 7 } => new ImagensProduct(){
                    ProcessorId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 8 } => new ImagensProduct(){
                    TowerId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                { Id: 9 } => new ImagensProduct(){
                    VideoBoardId = idProduct,
                    ImageName = file.FileName,
                    Pathkey = $"{imageCategoryDto.Name}/{idProduct}"
                },
                _ => throw new NotImplementedException()
            };
            return model;
        }

        private async Task<bool> ValidateProductIdByCategoryName(ImagensProduct imagensProduct, ImageCategoryDto imageCategoryDto)
        {
            var result = imageCategoryDto.Name.ToLower() switch
            {
                "cooler" => await _coolerService.GetByIdAsync(imagensProduct.CoolerId ?? 0) is null ? false : true,
                "memoryram" => await _memoryRamService.GetByIdAsync(imagensProduct.MemoryRamId ?? 0) is null ? false : true,
                "memoryrom" => await _memoryRomService.GetByIdAsync(imagensProduct.MemoryRomId ?? 0) is null ? false : true,
                "monitorscreen" => await _monitorScreenService.GetByIdAsync(imagensProduct.MonitorScreenId ?? 0) is null ? false : true,
                "motherboard" => await _motherBoardService.GetByIdAsync(imagensProduct.MonitorScreenId ?? 0) is null ? false : true,
                "powersupply" => await _powerSupplyService.GetByIdAsync(imagensProduct.PowerSupplyId ?? 0) is null ? false : true,
                "processor" => await _processorService.GetByIdAsync(imagensProduct.ProcessorId ?? 0) is null ? false : true,
                "tower" => await _towerService.GetByIdAsync(imagensProduct.TowerId ?? 0) is null ? false : true,
                "videoboard" => await _videoBoardService.GetByIdAsync(imagensProduct.VideoBoardId ?? 0) is null ? false : true,
                _ => throw new NotImplementedException()
            };
            return result;
        }


    }
}