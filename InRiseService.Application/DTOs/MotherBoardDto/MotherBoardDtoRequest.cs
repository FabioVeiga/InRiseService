using System.ComponentModel.DataAnnotations;
using InRiseService.Application.DTOs.PriceDto;

namespace InRiseService.Application.DTOs.MotherBoardDto
{
    public class MotherBoardDtoRequest
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Display(Name = "Socket Procssador")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Socket { get; set; } = default!;

        [Display(Name = "Socket Mémoria")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SocketMemory { get; set; } = default!;

        [Display(Name = "Socket Mémoria de Vídeo")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SocketMemoryVideo { get; set; } = default!;

        [Display(Name = "Socket SSD")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SocketSSD { get; set; } = default!;

        [Display(Name = "Socket M.2")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string SocketM2 { get; set; } = default!;

        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Valor (Classificação)")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ValueClassification { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public PriceRequestDto Price { get; set; } = default!;
    }
}