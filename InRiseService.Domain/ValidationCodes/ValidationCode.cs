using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Enums;
using InRiseService.Domain.Users;

namespace InRiseService.Domain.ValidationCodes
{
    public class ValidationCode : BaseDomain
    {
        [Display(Name = "Códido de validação")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Code { get; set; }

        [Display(Name = "Valido até")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public DateTime ExpirateAt { get; set; } = DateTime.Now.AddHours(5);

        public bool IsValidate { get; set; } = false;

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public EnumTypeCodeValidation TypeCode { get; set; } 
        
        [ForeignKey("User")]
        public int UserId { get;  set; }

        [JsonIgnore]
        public User? User { get; set; }

    }
}