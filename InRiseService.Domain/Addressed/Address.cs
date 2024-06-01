using System.Text.Json.Serialization;
using InRiseService.Domain.UsersAddress;

namespace InRiseService.Domain.Addressed
{
    public class Address : BaseDomain
    {
        public string PostalCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Province { get; set; } = null!;
        
        [JsonIgnore]
        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}