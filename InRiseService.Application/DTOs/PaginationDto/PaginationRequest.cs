using System.ComponentModel.DataAnnotations;

namespace InRiseService.Application.DTOs.PaginationDto
{
    public class PaginationRequest
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int PageIndex { get; set; } = 1;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int PageSize { get; set; } = 10;
    }
}