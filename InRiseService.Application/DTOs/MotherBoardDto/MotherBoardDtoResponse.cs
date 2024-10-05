using InRiseService.Application.DTOs.ImageProductDto;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MotherBoardDto
{
    public class MotherBoardDtoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Socket { get; set; } = default!;
        public string SocketMemory { get; set; } = default!;
        public string SocketMemoryVideo { get; set; } = default!;
        public string SocketSSD { get; set; } = default!;
        public string SocketM2 { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public int ValueClassification { get; set; }
        public ICollection<ImageProductResponseDto>? Images { get; set; }
        public PriceResponseDto Price { get; set; } = default!;
    }
}