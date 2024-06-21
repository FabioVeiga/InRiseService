namespace InRiseService.Application.DTOs.ImageProductDto
{
    public class ImageProductResponseDto
    {
        public int Id { get; set; }
        public string Pathkey { get; set; } = default!;
        public string ImageName { get; set; } = default!;
    }
}