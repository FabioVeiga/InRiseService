using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InRiseService.Domain.Enums;

namespace InRiseService.Domain.ValidationCodes
{
    public class ValidationCode : BaseDomain
    {
        [Display(Name = "Códido de validação")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Code { get; set; }

        [Display(Name = "Valido até")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public DateTime ExpiratitedAt { get; set; } = DateTime.Now.AddHours(5);

        public bool IsValidate { get; set; } = false;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public EnumTypeCodeValidation TypeCode { get; set; } 
        
        [ForeignKey("User")]
        public Guid UserId { get;  set; }

    }
}