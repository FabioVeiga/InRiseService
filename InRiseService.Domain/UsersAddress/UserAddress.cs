using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Addressed;
using InRiseService.Domain.Users;

namespace InRiseService.Domain.UsersAddress
{
    public class UserAddress : BaseDomain
    {

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [MinLength(5, ErrorMessage = "{0} deve conter no mínimo {1} caracteres!")]
        public string Street { get; set; } = default!;

        [Display(Name = "Numero")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public int Number { get; set; } = default!;

        [Display(Name = "Observação")]
        public string Observation { get; set; } = default!;

        [Display(Name = "Padrão")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool IsDefault { get; set; } = default!;

        [Display(Name = "Faturamento")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        public bool IsBilling { get; set; } = default!;
        
        [ForeignKey("User")]
        public int UserId { get; set; } = default!;
        
        [JsonIgnore]
        public User User { get; set; } = default!;

        [ForeignKey("Address")]
        public int AddressId { get; set; } = default!;
        
        [JsonIgnore]
        public Address Address { get; set; } = default!;
    }
}