using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MemoryRomDto
{
    public class MemoryRomResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Socket { get; set; } = default!;
        public double VelocityRead { get; set; } = default!;
        public double VelocityWrite { get; set; } = default!;
        public int Capacity { get; set; }
        public int Potency { get; set; }
        public PriceRequestDto Price { get; set; } = default!;
    }
}