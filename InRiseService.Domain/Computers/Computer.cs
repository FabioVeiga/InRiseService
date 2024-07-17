using System.ComponentModel.DataAnnotations;

namespace InRiseService.Domain.Computers
{
    public class Computer : BaseDomain
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(1, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        [MaxLength(100, ErrorMessage = "{0} deve conter no máximo {1} caracteres!")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int ProcessadorId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MotherBoardId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int TowerId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRamSlot01Id { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRamSlot02Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRomHHDId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRomSSDId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MemoryRomSSDM2Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int VideoBoardId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int PowerSupplyId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int CoolerId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int MonitorScreenId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Preço de Venda")]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} precisa ser maior que {1}")]
        public decimal FinalPrice { get; set; }
    }
}